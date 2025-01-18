using System.IO;
using System.Text;
using SBnDota2ModExporter.GUI;

namespace SBnDota2ModExporter.AddonExportCommands;

public class CopyDirectoryCommand(
  string addonOutputDirectoryFullPath,
  IProgress<AddonExportProgress> progress,
  string pathToDirectory,
  bool isCopySubfolders,
  bool isCopyOnlyContentOfDirectory)
{
  public void Execute()
  {
    progress.Report(new AddonExportProgress(BuildInitialProgressMessage()));

    var pathToDirectoryInfo = new DirectoryInfo(pathToDirectory);
    if (pathToDirectoryInfo.Exists is false)
    {
      progress.Report(new AddonExportProgress($"Directory not exist:{Environment.NewLine}" +
                                              $"'{pathToDirectory}'{Environment.NewLine}" +
                                              Constants.SKIP_COMMAND_TEXT,
        Constants.RTB_FOREGROUND_COLOR_WARNING));

      return;
    }

    var outputDirectoryInfo = new DirectoryInfo(addonOutputDirectoryFullPath);

    if (isCopyOnlyContentOfDirectory)
      FileManager.CopyDirectoryContent(pathToDirectoryInfo, outputDirectoryInfo, isCopySubfolders);
    else
      FileManager.CopyDirectory(pathToDirectoryInfo, outputDirectoryInfo, isCopySubfolders);

    progress.Report(new AddonExportProgress("Directory copying finished.", Constants.RTB_FOREGROUND_COLOR_SUCCESS));
  }

  private string BuildInitialProgressMessage()
  {
    var sb = new StringBuilder();
    sb.Append("Attempting to copy directory");

    if (isCopySubfolders)
      sb.Append(" (with subdirectories)");

    sb.Append(':');
    sb.AppendLine();

    sb.Append($"'{pathToDirectory}'");
    sb.AppendLine();

    sb.Append("into:");
    sb.AppendLine();

    sb.Append($"'{addonOutputDirectoryFullPath}'");
    sb.Append('.');

    return sb.ToString();
  }
}