using System.IO;
using System.Text;
using SBnDota2ModExporter.GUI;

namespace SBnDota2ModExporter.AddonExportCommands;

public static class CopyDirectoryCommand
{
  public static void Execute(string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress, string pathToDirectory, bool isCopySubfolders)
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

    progress.Report(new AddonExportProgress(sb.ToString()));

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

    FileManager.CopyDirectory(pathToDirectoryInfo, outputDirectoryInfo, isCopySubfolders);

    progress.Report(new AddonExportProgress("Directory copying finished.", Constants.RTB_FOREGROUND_COLOR_SUCCESS));
  }
}