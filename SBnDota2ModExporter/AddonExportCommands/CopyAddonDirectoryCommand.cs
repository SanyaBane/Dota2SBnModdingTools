using System.IO;
using System.Text;
using SBnDota2ModExporter.GUI;

namespace SBnDota2ModExporter.AddonExportCommands;

public class CopyAddonDirectoryCommand(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress, string pathToAddonDirectory, bool isCopySubfolders)
{
  public void Execute()
  {
    progress.Report(new AddonExportProgress(BuildInitialProgressMessage()));

    var fullPathToDirectory = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dota2AddonName, pathToAddonDirectory);
    var pathToDirectoryInfo = new DirectoryInfo(fullPathToDirectory);
    if (pathToDirectoryInfo.Exists is false)
    {
      progress.Report(new AddonExportProgress($"Addon directory not exist:{Environment.NewLine}" +
                                              $"'{pathToAddonDirectory}'{Environment.NewLine}" +
                                              Constants.SKIP_COMMAND_TEXT,
        Constants.RTB_FOREGROUND_COLOR_WARNING));

      return;
    }

    var outputDirectoryInfo = new DirectoryInfo(addonOutputDirectoryFullPath);

    FileManager.CopyDirectory(pathToDirectoryInfo, outputDirectoryInfo, isCopySubfolders);

    progress.Report(new AddonExportProgress("Addon directory copying finished.", Constants.RTB_FOREGROUND_COLOR_SUCCESS));
  }

  private string BuildInitialProgressMessage()
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

    return sb.ToString();
  }
}