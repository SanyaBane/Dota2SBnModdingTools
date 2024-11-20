using System.IO;
using System.Text;
using SBnDota2ModExporter.GUI;

namespace SBnDota2ModExporter.AddonExportCommands;

public static class CopyAddonDirectoryCommand
{
  public static void Execute(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress, string pathToAddonDirectory, bool isCopySubfolders)
  {
    var sb = new StringBuilder();
    sb.Append("Attempting to copy addon directory");

    if (isCopySubfolders)
      sb.Append(" (with subdirectories)");

    sb.Append(':');
    sb.AppendLine();

    sb.Append($"'{pathToAddonDirectory}'");
    sb.AppendLine();

    sb.Append("into:");
    sb.AppendLine();

    sb.Append($"'{addonOutputDirectoryFullPath}'");
    sb.Append('.');

    progress.Report(new AddonExportProgress(sb.ToString()));

    var fullPathToDirectory = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dota2AddonName, pathToAddonDirectory);
    var pathToDirectoryInfo = new DirectoryInfo(fullPathToDirectory);
    if (pathToDirectoryInfo.Exists is false)
    {
      progress.Report(new AddonExportProgress($"Error. Addon directory not exist:{Environment.NewLine}" +
                                              $"'{pathToAddonDirectory}'{Environment.NewLine}" +
                                              "Skip.",
        Constants.RTB_FOREGROUND_COLOR_WARNING));

      return;
    }

    var outputDirectoryInfo = new DirectoryInfo(addonOutputDirectoryFullPath);

    FileManager.CopyDirectory(pathToDirectoryInfo, outputDirectoryInfo, isCopySubfolders);

    progress.Report(new AddonExportProgress("Addon directory copying finished.", Constants.RTB_FOREGROUND_COLOR_SUCCESS));
  }
}