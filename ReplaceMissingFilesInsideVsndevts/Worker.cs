using CommonLib;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using SteamDatabase.ValvePak;
using ValveResourceFormat.Serialization.KeyValues;

namespace ReplaceMissingFilesInsideVsndevts;

public class Worker
{
  public void DoWork(string[] fullPathToFiles)
  {
    var settings = GetSettings();

    var dota2Executable = new FileInfo(settings.FullPathToDota2ExecutableFile);
    if (dota2Executable.Exists is false)
    {
      Console.WriteLine($"{Environment.NewLine}" +
                        $"Error: Set correct path to 'dota2.exe' file in 'appsettings.json' (it might look like this: 'C:\\Program Files\\Steam\\steamapps\\common\\dota 2 beta\\game\\bin\\win64\\dota2.exe'){Environment.NewLine}" +
                        $"{Constants.TEXT_PRESS_ENTER_TO_EXIT}");
      Console.ReadLine();
      return;
    }

    Console.WriteLine($"Dota 2 directory: '{dota2Executable.Directory}'");

    foreach (var fullPathToFile in fullPathToFiles)
    {
      ParseSingleFile(fullPathToFile, dota2Executable, settings);
    }

    Console.WriteLine(Constants.TEXT_PRESS_ENTER_TO_EXIT);
    Console.ReadLine();
  }

  private static void ParseSingleFile(string fullPathToPassedVsndevtsFile, FileInfo dota2Executable, ReplaceMissingFilesInsideVsndevtsSettings settings)
  {
    var passedVsndevtsFile = new FileInfo(fullPathToPassedVsndevtsFile);

    Console.WriteLine($"Program will parse file '{passedVsndevtsFile.Name}' and search for not existing audio files...");

    var resultTryDoWork = TryDoWork(passedVsndevtsFile, dota2Executable, settings.ReplaceValue);
    if (resultTryDoWork.IsFailure)
    {
      Console.WriteLine($"{Environment.NewLine}" +
                        $"Error: {resultTryDoWork.Error}" +
                        $"{Constants.TEXT_PRESS_ENTER_TO_EXIT}");
      Console.ReadLine();
      return;
    }

    var consoleDefaultForegroundColor = Console.ForegroundColor;

    if (resultTryDoWork.Value.CountFilesReplaced > 0)
    {
      using var indentedTextWriter = new ValveResourceFormat.IndentedTextWriter();
      resultTryDoWork.Value.Kv3File.WriteText(indentedTextWriter);

      var vsndevtsFileDirectory = passedVsndevtsFile.Directory;
      var newFileName = Path.GetFileNameWithoutExtension(passedVsndevtsFile.FullName) + "_modified" + passedVsndevtsFile.Extension;
      var fullPathToModifiedFile = Path.Combine(vsndevtsFileDirectory.FullName, newFileName);

      File.WriteAllText(fullPathToModifiedFile, indentedTextWriter.ToString());

      Console.ForegroundColor = ConsoleColor.Yellow;
      var textReferences = resultTryDoWork.Value.CountFilesReplaced == 1 ? "reference" : "references";
      Console.WriteLine($"{Environment.NewLine}" +
                        $"There was {resultTryDoWork.Value.CountFilesReplaced} {textReferences} to missing files in this '.{ConstantsCommon.VSNDEVTS_FORMAT}' file.{Environment.NewLine}" +
                        $"Created '{newFileName}' file, where all missing references replaced by '{settings.ReplaceValue}'.");
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"{Environment.NewLine}" +
                        $"There is no references to missing files in this '.{ConstantsCommon.VSNDEVTS_FORMAT}' file. Everything is fine.");
    }

    Console.ForegroundColor = consoleDefaultForegroundColor;
    Console.WriteLine();
    Console.WriteLine(" ========== ");
    Console.WriteLine();
  }

  private static Result<ReplaceMissingFilesResult?> TryDoWork(FileInfo passedVsndevtsFile, FileInfo dota2ExecutableFile, string replaceValue)
  {
    var resultCreateDota2GameMainInfo = Dota2GameMainInfo.CreateDota2GameMainInfo(dota2ExecutableFile.FullName);
    if (resultCreateDota2GameMainInfo.IsFailure)
    {
      throw new NotImplementedException();
    }

    var dota2GameMainInfo = resultCreateDota2GameMainInfo.Value;

    var resultGetDotaAddonInfo = Dota2AddonInfo.GetDotaAddonInfo(passedVsndevtsFile, dota2GameMainInfo);
    if (resultGetDotaAddonInfo.IsFailure)
    {
      return Result.Failure<ReplaceMissingFilesResult?>(resultGetDotaAddonInfo.Error);
    }

    var dotaAddonInfo = resultGetDotaAddonInfo.Value;
    var pak01DirVpkFullPath = dotaAddonInfo.Dota2GameMainInfo.Pak01DirVpkFileInfo;

    var package = new Package();
    package.OptimizeEntriesForBinarySearch(StringComparison.OrdinalIgnoreCase);
    package.Read(pak01DirVpkFullPath.FullName);

    var parsedKvFile = KeyValues3.ParseKVFile(passedVsndevtsFile.FullName);

    int counterFilesReplaced = 0;

    Console.WriteLine($"This file contains {parsedKvFile.Root.Count} sound events.{Environment.NewLine}");

    foreach (var keyValuePair in parsedKvFile.Root.Properties)
    {
      if (keyValuePair.Value is not KVValue kvValue)
        continue;

      if (kvValue.Value is not KVObject kvObject)
        continue;

      var kvValuesToReplace = new Dictionary<string, KVValue>();

      foreach (var kvObjectProperty in kvObject.Properties)
      {
        if (kvObjectProperty.Key != "vsnd_files")
          continue;

        if (kvObjectProperty.Value is not KVValue vsndFilesKvValue)
          continue;

        if (vsndFilesKvValue.Type == KVType.STRING)
        {
          var vsndevtsFileRelativePath = vsndFilesKvValue.Value.ToString();
          var isVsndFileExists = IsVsndFileExists(dotaAddonInfo, dotaAddonInfo.DotaAddonContentDirectoryInfo, vsndevtsFileRelativePath, package);
          if (isVsndFileExists is false)
          {
            Console.WriteLine($"'{keyValuePair.Key}' - file not found: '{vsndevtsFileRelativePath}'.");

            kvValuesToReplace[kvObjectProperty.Key] = new KVValue(vsndFilesKvValue.Type, replaceValue);
          }
        }
        else if (vsndFilesKvValue.Value is KVObject kvObject2)
        {
          var kvValuesToReplace2 = ParseVsndFilesArrayNode(dotaAddonInfo, replaceValue, kvObject2, dotaAddonInfo.DotaAddonContentDirectoryInfo, keyValuePair, package);

          foreach (var valuePair in kvValuesToReplace2)
          {
            kvObject2.Properties[valuePair.Key] = valuePair.Value;
            counterFilesReplaced++;
          }
        }
      }

      foreach (var valuePair in kvValuesToReplace)
      {
        kvObject.Properties[valuePair.Key] = valuePair.Value;
        counterFilesReplaced++;
      }
    }

    return Result.Success<ReplaceMissingFilesResult?>(new ReplaceMissingFilesResult(parsedKvFile, counterFilesReplaced));
  }

  private static Dictionary<string, KVValue> ParseVsndFilesArrayNode(Dota2AddonInfo dota2AddonInfo, string replaceValue, KVObject kvObject2, DirectoryInfo dotaAddonDirectory, KeyValuePair<string, KVValue> keyValuePair, Package package)
  {
    var kvValuesToReplace = new Dictionary<string, KVValue>();

    int replacedFilesCounter = 0;

    foreach (var vsndFileProperty in kvObject2.Properties)
    {
      if (vsndFileProperty.Value is not KVValue singleVsndFileKvValue)
        continue;

      var vsndevtsFileRelativePath = singleVsndFileKvValue.Value.ToString();
      var isVsndFileExists = IsVsndFileExists(dota2AddonInfo, dotaAddonDirectory, vsndevtsFileRelativePath, package);
      if (isVsndFileExists is false)
      {
        Console.WriteLine($"'{keyValuePair.Key}' - file not found: '{vsndevtsFileRelativePath}'.");

        kvValuesToReplace[vsndFileProperty.Key] = new KVValue(singleVsndFileKvValue.Type, replaceValue);
      }
    }

    return kvValuesToReplace;
  }

  private static bool IsVsndFileExists(Dota2AddonInfo dota2AddonInfo, DirectoryInfo dotaAddonDirectory, string vsndevtsFileName, Package package)
  {
    var vsndevtsFileFullPath = Path.Combine(dotaAddonDirectory.FullName, vsndevtsFileName);
    var vsndevtsFile = new FileInfo(vsndevtsFileFullPath);

    var vsndevtsFileDirectory = vsndevtsFile.Directory;
    if (vsndevtsFileDirectory.Exists)
    {
      var isVsndFileExistsInAddonFolder = IsVsndFileExistsInAddonFolder(vsndevtsFileDirectory, vsndevtsFile);
      if (isVsndFileExistsInAddonFolder)
        return true;
    }

    var isVsndFileExistsInDota2Files = IsVsndFileExistsInDota2Files(dota2AddonInfo, vsndevtsFile, package);
    return isVsndFileExistsInDota2Files;
  }

  private static bool IsVsndFileExistsInAddonFolder(DirectoryInfo vsndevtsFileDirectory, FileInfo vsndevtsFile)
  {
    return vsndevtsFileDirectory.EnumerateFiles().Any(x => x.Name.IndexOf(Path.GetFileNameWithoutExtension(vsndevtsFile.FullName), StringComparison.InvariantCultureIgnoreCase) == 0);
  }

  private static bool IsVsndFileExistsInDota2Files(Dota2AddonInfo dota2AddonInfo, FileInfo vsndevtsFile, Package package)
  {
    var sameVsndevtsInsidePak01DirFile = VsndevtsInsideDota2Reader.FindFileInsideDota2(dota2AddonInfo, vsndevtsFile, package);
    return sameVsndevtsInsidePak01DirFile != null;
  }

  private ReplaceMissingFilesInsideVsndevtsSettings GetSettings()
  {
    var entryAssemblyLocation = new FileInfo(AppContext.BaseDirectory);

    var builder = new ConfigurationBuilder();
    builder.SetBasePath(entryAssemblyLocation.Directory.FullName)
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    var configuration = builder.Build();

    string fullPathToDota2ExecutableFile = configuration["PathToDota2ExeFile"];
    string replaceValue = configuration["ReplaceValue"];

    return new ReplaceMissingFilesInsideVsndevtsSettings(fullPathToDota2ExecutableFile, replaceValue);
  }
}