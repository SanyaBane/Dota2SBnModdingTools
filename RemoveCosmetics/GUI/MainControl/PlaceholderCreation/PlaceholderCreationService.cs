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
  private const string vpkCreatorDirectoryName = "VPK Creator";

  public async Task<Result> CreateVpkFileWithPlaceholderModels(IProgress<PlaceholderCreationProgress> progress, string[] directoryNames, string safeFileFullPath)
  {
    DirectoryInfo? tempPak01DirDirectory = null;
    FileInfo? tempCreatedVpkFile = null;

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
      if (!vpkCreatorDirectory.Exists)
        return Result.Failure(
          $"Not found '{vpkCreatorDirectoryName}' folder in directory with application:" +
          $"{Environment.NewLine}" +
          $"'{fullPathToVPKCreatorDirectory}'.");

      var tmpPak01DirName = Guid.NewGuid().ToString().Substring(0, 4);
      tempPak01DirDirectory = new DirectoryInfo(Path.Combine(fullPathToVPKCreatorDirectory, tmpPak01DirName));
      if (tempPak01DirDirectory.Exists)
      {
        Directory.Delete(tempPak01DirDirectory.FullName, true);
      }

      progress.Report(new PlaceholderCreationProgress
      {
        Text = "Creating temporary directory for placeholder files.",
      });

      tempPak01DirDirectory.Create();

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

          var fullPathForPlaceholder = Path.Combine(tempPak01DirDirectory.FullName, packageEntry.DirectoryName, packageEntry.GetFileName());
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
      var arguments = $"\"{tempPak01DirDirectory.FullName}\"";
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

      tempCreatedVpkFile = new FileInfo(Path.Combine(vpkCreatorDirectory.FullName, tempPak01DirDirectory.Name + ".vpk"));
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
                 $"Please consider to move directory of this program to root of your hard drive (for example 'D:\\RemoveCosmetics\\{Constants_General.PROGRAM_TITLE}.exe').",
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
      if (tempPak01DirDirectory != null && Directory.Exists(tempPak01DirDirectory.FullName))
      {
        progress.Report(new PlaceholderCreationProgress
        {
          Text = "Deleting temporary directory."
        });

        Directory.Delete(tempPak01DirDirectory.FullName, true);
      }

      if (tempCreatedVpkFile != null && File.Exists(tempCreatedVpkFile.FullName))
      {
        progress.Report(new PlaceholderCreationProgress
        {
          Text = "Deleting temporary vpk file."
        });

        File.Delete(tempCreatedVpkFile.FullName);
      }
    }
  }
}