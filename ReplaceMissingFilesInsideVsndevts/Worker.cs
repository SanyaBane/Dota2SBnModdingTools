﻿using CommonLib;
using Microsoft.Extensions.Configuration;
using SteamDatabase.ValvePak;
using ValveResourceFormat.Serialization.KeyValues;

namespace ReplaceMissingFilesInsideVsndevts;

public class Worker
{
  public void DoWork(string fullPathToPassedVsndevtsFile)
  {
    var entryAssemblyLocation = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location);

    var builder = new ConfigurationBuilder();
    builder.SetBasePath(entryAssemblyLocation.Directory.FullName)
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    IConfigurationRoot configuration = builder.Build();


    string fullPathToDota2Executable = configuration["PathToDota2ExeFile"];
    string replaceValue = configuration["ReplaceValue"];

    var dota2Executable = new FileInfo(fullPathToDota2Executable);
    if (dota2Executable.Exists is false)
    {
      Console.WriteLine($"{Environment.NewLine}" +
                        $"Error: Set correct path to 'dota2.exe' file in 'appsettings.json' (it might look like this: 'C:\\Program Files\\Steam\\steamapps\\common\\dota 2 beta\\game\\bin\\win64\\dota2.exe'){Environment.NewLine}" +
                        $"{Constants.TEXT_PRESS_ENTER_TO_EXIT}");
      Console.ReadLine();
      return;
    }

    Console.WriteLine($"Dota 2 directory: '{dota2Executable.Directory}'");

    var passedVsndevtsFile = new FileInfo(fullPathToPassedVsndevtsFile);

    Console.WriteLine($"Program will parse file '{passedVsndevtsFile.Name}' and search for not existing audio files...");

    var resultTryDoWork = TryDoWork(passedVsndevtsFile, dota2Executable, replaceValue);
    if (resultTryDoWork.Failure)
    {
      Console.WriteLine($"{Environment.NewLine}" +
                        $"Error: {resultTryDoWork.ErrorMessage}" +
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
                        $"There was {resultTryDoWork.Value.CountFilesReplaced} {textReferences} to missing files in this '.vsndevts' file.{Environment.NewLine}" +
                        $"Created '{newFileName}' file, where all missing references replaced by '{replaceValue}'.");
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"{Environment.NewLine}" +
                        "There is no references to missing files in this '.vsndevts' file. Everything is fine.");
    }

    Console.ForegroundColor = consoleDefaultForegroundColor;
    Console.WriteLine(Constants.TEXT_PRESS_ENTER_TO_EXIT);
    Console.ReadLine();
  }

  private static Result<ReplaceMissingFilesResult?> TryDoWork(FileInfo passedVsndevtsFile, FileInfo dota2Executable, string replaceValue)
  {
    var resultGetDotaAddonInfo = DotaAddonInfo.GetDotaAddonInfo(passedVsndevtsFile, dota2Executable);
    if (resultGetDotaAddonInfo.Failure)
    {
      return new Result<ReplaceMissingFilesResult?>(false, resultGetDotaAddonInfo.ErrorMessage);
    }

    var dotaAddonInfo = resultGetDotaAddonInfo.Value;

    string pak01DirFullPath = Path.Combine(dotaAddonInfo.Dota2Directory.FullName, "game", "dota", "pak01_dir.vpk");
    var pak01DirVpkFullPath = new FileInfo(pak01DirFullPath);

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
          var isVsndFileExists = IsVsndFileExists(dotaAddonInfo, dotaAddonInfo.DotaAddonDirectory, vsndevtsFileRelativePath, package);
          if (isVsndFileExists is false)
          {
            Console.WriteLine($"'{keyValuePair.Key}' - file not found: '{vsndevtsFileRelativePath}'.");

            kvValuesToReplace[kvObjectProperty.Key] = new KVValue(vsndFilesKvValue.Type, replaceValue);
          }
        }
        else if (vsndFilesKvValue.Value is KVObject kvObject2)
        {
          var kvValuesToReplace2 = ParseVsndFilesArrayNode(dotaAddonInfo, replaceValue, kvObject2, dotaAddonInfo.DotaAddonDirectory, keyValuePair, package);

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

    return new Result<ReplaceMissingFilesResult?>(true, new ReplaceMissingFilesResult(parsedKvFile, counterFilesReplaced));
  }

  private static Dictionary<string, KVValue> ParseVsndFilesArrayNode(DotaAddonInfo dotaAddonInfo, string replaceValue, KVObject kvObject2, DirectoryInfo dotaAddonDirectory, KeyValuePair<string, KVValue> keyValuePair, Package package)
  {
    var kvValuesToReplace = new Dictionary<string, KVValue>();

    int replacedFilesCounter = 0;

    foreach (var vsndFileProperty in kvObject2.Properties)
    {
      if (vsndFileProperty.Value is not KVValue singleVsndFileKvValue)
        continue;

      var vsndevtsFileRelativePath = singleVsndFileKvValue.Value.ToString();
      var isVsndFileExists = IsVsndFileExists(dotaAddonInfo, dotaAddonDirectory, vsndevtsFileRelativePath, package);
      if (isVsndFileExists is false)
      {
        Console.WriteLine($"'{keyValuePair.Key}' - file not found: '{vsndevtsFileRelativePath}'.");

        kvValuesToReplace[vsndFileProperty.Key] = new KVValue(singleVsndFileKvValue.Type, replaceValue);
      }
    }

    return kvValuesToReplace;
  }

  private static bool IsVsndFileExists(DotaAddonInfo dotaAddonInfo, DirectoryInfo dotaAddonDirectory, string vsndevtsFileName, Package package)
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

    var isVsndFileExistsInDota2Files = IsVsndFileExistsInDota2Files(dotaAddonInfo, vsndevtsFile, package);
    return isVsndFileExistsInDota2Files;
  }

  private static bool IsVsndFileExistsInAddonFolder(DirectoryInfo vsndevtsFileDirectory, FileInfo vsndevtsFile)
  {
    return vsndevtsFileDirectory.EnumerateFiles().Any(x => x.Name.IndexOf(Path.GetFileNameWithoutExtension(vsndevtsFile.FullName), StringComparison.InvariantCultureIgnoreCase) == 0);
  }

  private static bool IsVsndFileExistsInDota2Files(DotaAddonInfo dotaAddonInfo, FileInfo vsndevtsFile, Package package)
  {
    var sameVsndevtsInsidePak01DirFile = VsndevtsInsideDota2Reader.FindFileInsideDota2(dotaAddonInfo, vsndevtsFile, package);
    return sameVsndevtsInsidePak01DirFile != null;
  }
}