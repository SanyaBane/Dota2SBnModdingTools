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
  private const string relativePathToPlaceholderFile = "Resources/EmptyPlaceholder.vmdl_c";
  private const string vpkCreatorDirectoryName = "VPK Creator";

  public async Task<Result> CreateVpkFileWithPlaceholderModels(IProgress<PlaceholderCreationProgress> progress, string[] directoryNames, string safeFileFullPath)
  {
    DirectoryInfo? tempPak01DirDirectory = null;
    FileInfo? tempCreatedVpkFile = null;

    try
    {
      string placeholderFileFullPath = Path.Combine(Environment.CurrentDirectory, relativePathToPlaceholderFile);
      var fi = new FileInfo(placeholderFileFullPath);
      if (!fi.Exists)
      {
        return Result.Failure($"Not found placeholder file in directory with application:" +
                              $"{Environment.NewLine}" +
                              $"'{placeholderFileFullPath}'.");
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

      tempPak01DirDirectory.Create();
      progress.Report(new PlaceholderCreationProgress
      {
        Text = "Created temp directory.",
      });

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

      bool somePlaceholderFilesExceed255Characters = false;
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

          File.Copy(placeholderFileFullPath, fullPathForPlaceholder);
        }
      }

      progress.Report(new PlaceholderCreationProgress
      {
        Text = "Finished copying placeholder files into temp directory.",
      });

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
          Text = $"VpkCreator: {args.Data}",
        });
      };

      process.ErrorDataReceived += (sender, args) =>
      {
        if (string.IsNullOrEmpty(args.Data))
          return;

        progress.Report(new PlaceholderCreationProgress
        {
          Text = $"VpkCreator ERROR: {args.Data}", 
          ForegroundColor = Brushes.Red,
        });
      };

      process.Start();
      process.BeginOutputReadLine();
      process.BeginErrorReadLine();

      await process.WaitForExitAsync();

      tempCreatedVpkFile = new FileInfo(Path.Combine(vpkCreatorDirectory.FullName, tempPak01DirDirectory.Name + ".vpk"));

      if (!tempCreatedVpkFile.Exists)
      {
        return Result.Failure($"Temporary vpk file '{tempCreatedVpkFile.FullName}' not exists.");
      }

      File.Copy(tempCreatedVpkFile.FullName, safeFileFullPath, true);
      progress.Report(new PlaceholderCreationProgress
      {
        Text = "Copying temp vpk file to specified location."
      });

      if (!File.Exists(safeFileFullPath))
        return Result.Failure($"Failed to copy temporary vpk file to '{tempCreatedVpkFile.FullName}'");

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
        Directory.Delete(tempPak01DirDirectory.FullName, true);
        progress.Report(new PlaceholderCreationProgress
        {
          Text = "Deleted temporary directory."
        });
      }

      if (tempCreatedVpkFile != null && File.Exists(tempCreatedVpkFile.FullName))
      {
        File.Delete(tempCreatedVpkFile.FullName);
        progress.Report(new PlaceholderCreationProgress
        {
          Text = "Deleted temporary vpk file."
        });
      }
    }
  }
}