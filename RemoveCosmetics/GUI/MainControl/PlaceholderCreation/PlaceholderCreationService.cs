using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using CSharpFunctionalExtensions;
using RemoveCosmetics.Constants;
using RemoveCosmetics.Settings;
using SteamDatabase.ValvePak;

namespace RemoveCosmetics.GUI.MainControl.PlaceholderCreation;

public class PlaceholderCreationService
{
  private const string vpkCreatorDirectoryName = "VPK_Creator";

  public async Task<Result> CreateVpkFileWithPlaceholderModels(IProgress<PlaceholderCreationProgress> progress, string[] directoryNames, string safeFileFullPath)
  {
    DirectoryInfo? tempRootDirectoryForVpkCreation = null;
    FileInfo? tempCreatedVpkFile = null;

    var unpackVpkCreatorFilesResult = await UnpackVpkCreatorResources();
    if (unpackVpkCreatorFilesResult.IsFailure)
      return Result.Failure("Was not able to unpack VpkCreator related resources");

    try
    {
      var resourceUri = new Uri(ConstantsResources.EMPTY_PLACEHOLDER_FILE, UriKind.Relative);

      var resourceInfo = Application.GetResourceStream(resourceUri);
      if (resourceInfo == null)
        return Result.Failure("Resource not found: " + ConstantsResources.EMPTY_PLACEHOLDER_FILE);

      // cache placeholder file into memory
      byte[]? cachedResourceData;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        await resourceInfo.Stream.CopyToAsync(memoryStream);
        cachedResourceData = memoryStream.ToArray();
      }

      string fullPathToVPKCreatorDirectory = Path.Combine(Environment.CurrentDirectory, vpkCreatorDirectoryName);
      var vpkCreatorDirectory = new DirectoryInfo(fullPathToVPKCreatorDirectory);

      var tmpPak01DirName = Guid.NewGuid().ToString().Substring(0, 4);
      tempRootDirectoryForVpkCreation = new DirectoryInfo(Path.Combine(fullPathToVPKCreatorDirectory, tmpPak01DirName));
      if (tempRootDirectoryForVpkCreation.Exists)
      {
        Directory.Delete(tempRootDirectoryForVpkCreation.FullName, true);
      }

      progress.Report(new PlaceholderCreationProgress
      {
        Text = "Creating temporary directory for placeholder files.",
      });

      tempRootDirectoryForVpkCreation.Create();

      var package = new Package();
      package.Read(SettingsManager.Instance.Dota2GameMainInfo.Pak01DirVpkFileInfo.FullName);

      var vmdlcEntries = package.Entries["vmdl_c"];
      var modelsHeroesEntries = vmdlcEntries
        .Where(x => x.DirectoryName.StartsWith("models/heroes") || x.DirectoryName.StartsWith("models/items")).ToArray();

      var placeholderFileExceptions = SettingsManager.Instance.RemoveCosmeticsConfig.PlaceholderFileExceptions;
      var placeholderFileNameExceptions = placeholderFileExceptions.Where(x => !x.IsRegexPattern).ToArray();
      var placeholderFilePatternExceptions = placeholderFileExceptions.Where(x => x.IsRegexPattern).ToArray();

      var placeholderDirectoryExceptions = SettingsManager.Instance.RemoveCosmeticsConfig.PlaceholderDirectoryExceptions;
      var placeholderDirectoryNameExceptions = placeholderDirectoryExceptions.Where(x => !x.IsRegexPattern).ToArray();
      var placeholderDirectoryPatternExceptions = placeholderDirectoryExceptions.Where(x => x.IsRegexPattern).ToArray();

      progress.Report(new PlaceholderCreationProgress
      {
        Text = "Start copying placeholder files into temporary directory.",
      });

      var stopwatchCopyingPlaceholderFiles = Stopwatch.StartNew();
      bool somePlaceholderFilesExceed255Characters = false;
      int copiedPlaceholdersCounter = 0;
      foreach (var directoryName in directoryNames)
      {
        var heroesDirectory = $"models/heroes/{directoryName}";
        var itemsDirectory = $"models/items/{directoryName}";

        var matches = modelsHeroesEntries
          .Where(x => x.DirectoryName == heroesDirectory
                      || x.DirectoryName.StartsWith(heroesDirectory + "/")
                      || x.DirectoryName == itemsDirectory
                      || x.DirectoryName.StartsWith(itemsDirectory + "/"))
          .ToArray();

        foreach (var packageEntry in matches)
        {
          if (placeholderDirectoryNameExceptions.Any(x => packageEntry.DirectoryName.StartsWith(x.Value)))
            continue;

          if (placeholderDirectoryPatternExceptions.Any(x => Regex.IsMatch(packageEntry.DirectoryName, $"^{x.Value}$")))
            continue;

          if (placeholderFileNameExceptions.Any(x => packageEntry.GetFullPath() == x.Value))
            continue;

          if (placeholderFilePatternExceptions.Any(x => Regex.IsMatch(packageEntry.GetFullPath(), $"^{x.Value}$")))
            continue;

          var fullPathForPlaceholder = Path.Combine(tempRootDirectoryForVpkCreation.FullName, packageEntry.DirectoryName, packageEntry.GetFileName());
          if (fullPathForPlaceholder.Length > 255)
          {
            somePlaceholderFilesExceed255Characters = true;
            continue;
          }

          var placeholderFileInfo = new FileInfo(fullPathForPlaceholder);
          var dir = placeholderFileInfo.Directory;

          if (!dir.Exists)
            dir.Create();

          await File.WriteAllBytesAsync(fullPathForPlaceholder, cachedResourceData);
          copiedPlaceholdersCounter++;
        }
      }

      stopwatchCopyingPlaceholderFiles.Stop();
      progress.Report(new PlaceholderCreationProgress
      {
        Text = $"Finished copying {copiedPlaceholdersCounter} placeholder files into temporary directory in {stopwatchCopyingPlaceholderFiles.Elapsed.TotalSeconds:N2} seconds.",
        ForegroundColor = Brushes.Green
      });

      progress.Report(new PlaceholderCreationProgress
      {
        Text = "Start packing placeholder files into Vpk.",
      });

      var stopwatchVpkCreation = Stopwatch.StartNew();
      var arguments = $"\"{tempRootDirectoryForVpkCreation.FullName}\"";
      var processStartInfo = new ProcessStartInfo(Path.Combine(vpkCreatorDirectory.FullName, "vpk.exe"))
      {
        Arguments = arguments,
        WorkingDirectory = vpkCreatorDirectory.FullName,
        UseShellExecute = false,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        CreateNoWindow = true,
      };

      var process = new Process()
      {
        StartInfo = processStartInfo
      };

      process.OutputDataReceived += (sender, args) =>
      {
        if (string.IsNullOrEmpty(args.Data))
          return;

        progress.Report(new PlaceholderCreationProgress
        {
          Text = $"VpkCreator OutputDataReceived: {args.Data}",
        });
      };

      process.ErrorDataReceived += (sender, args) =>
      {
        if (string.IsNullOrEmpty(args.Data))
          return;

        progress.Report(new PlaceholderCreationProgress
        {
          Text = $"VpkCreator ErrorDataReceived: {args.Data}",
          ForegroundColor = Brushes.Red,
        });
      };

      process.Start();
      process.BeginOutputReadLine();
      process.BeginErrorReadLine();

      await process.WaitForExitAsync();

      stopwatchVpkCreation.Stop();

      tempCreatedVpkFile = new FileInfo(Path.Combine(vpkCreatorDirectory.FullName, tempRootDirectoryForVpkCreation.Name + ".vpk"));
      if (!tempCreatedVpkFile.Exists)
      {
        return Result.Failure($"Failed to create temporary vpk file '{tempCreatedVpkFile.FullName}'.");
      }

      progress.Report(new PlaceholderCreationProgress
      {
        Text = $"Finished creation of vpk file in {stopwatchVpkCreation.Elapsed.TotalSeconds:N2} seconds.",
        ForegroundColor = Brushes.Green,
      });

      progress.Report(new PlaceholderCreationProgress
      {
        Text = "Copying temporary vpk file to specified location."
      });

      File.Copy(tempCreatedVpkFile.FullName, safeFileFullPath, true);

      if (!File.Exists(safeFileFullPath))
        return Result.Failure($"Failed to copy temporary vpk file to '{tempCreatedVpkFile.FullName}'");

      progress.Report(new PlaceholderCreationProgress
      {
        Text = "Finished copying temporary vpk file to specified location."
      });

      if (somePlaceholderFilesExceed255Characters)
      {
        progress.Report(new PlaceholderCreationProgress
        {
          Text = $"Some files could not be converted to VPK because their full path exceeds 255 characters.{Environment.NewLine}" +
                 $"Please consider to move directory of this program to root of your hard drive (for example 'D:\\RemoveCosmetics\\{ConstantsGeneral.PROGRAM_TITLE}.exe').",
          ForegroundColor = Brushes.OrangeRed,
          FontWeight = FontWeights.Bold,
        });
      }

      return Result.Success();
    }
    catch (Exception ex)
    {
      return Result.Failure(ex.ToString());
    }
    finally
    {
      if (tempRootDirectoryForVpkCreation != null && Directory.Exists(tempRootDirectoryForVpkCreation.FullName))
      {
        progress.Report(new PlaceholderCreationProgress
        {
          Text = "Deleting temporary directory."
        });

        Directory.Delete(tempRootDirectoryForVpkCreation.FullName, true);
      }

      if (tempCreatedVpkFile != null && File.Exists(tempCreatedVpkFile.FullName))
      {
        progress.Report(new PlaceholderCreationProgress
        {
          Text = "Deleting temporary vpk file."
        });

        File.Delete(tempCreatedVpkFile.FullName);
      }

      DeleteUnpackedVpkCreatorResources(unpackVpkCreatorFilesResult.Value);
    }
  }

  private async Task<Result<UnpackedVpkCreatorResources>> UnpackVpkCreatorResources()
  {
    var ret = new UnpackedVpkCreatorResources()
    {
      FullPathToDirectory = Path.Combine(Environment.CurrentDirectory, vpkCreatorDirectoryName),
    };

    try
    {
      if (!Directory.Exists(ret.FullPathToDirectory))
        Directory.CreateDirectory(ret.FullPathToDirectory);

      var vpkCreatorResources = ConstantsResources.GetVpkCreatorResources();
      foreach (var resourcePath in vpkCreatorResources)
      {
        var resourceUri = new Uri(resourcePath, UriKind.Relative);
        var resourceInfo = Application.GetResourceStream(resourceUri);

        if (resourceInfo == null)
        {
          throw new FileNotFoundException("Resource not found: " + resourcePath);
        }

        var outputFilePath = Path.Combine(ret.FullPathToDirectory, Path.GetFileName(resourceUri.OriginalString));
        var directoryPath = Path.GetDirectoryName(outputFilePath)!;
        if (!Directory.Exists(directoryPath))
          Directory.CreateDirectory(directoryPath);

        // Read from resource stream and write to file
        await using (Stream resourceStream = resourceInfo.Stream)
        await using (FileStream fileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
        {
          await resourceStream.CopyToAsync(fileStream);
        }

        ret.AddFullPathToFile(outputFilePath);
      }
    }
    catch (Exception ex)
    {
      DeleteUnpackedVpkCreatorResources(ret);

      return Result.Failure<UnpackedVpkCreatorResources>(ex.ToString());
    }

    return Result.Success(ret);
  }

  private void DeleteUnpackedVpkCreatorResources(UnpackedVpkCreatorResources unpackedVpkCreatorResources)
  {
    foreach (var unpackedVpkCreatorFile in unpackedVpkCreatorResources.GetFullPathToFiles())
    {
      File.Delete(unpackedVpkCreatorFile);
    }

    Directory.Delete(unpackedVpkCreatorResources.FullPathToDirectory);
  }
}