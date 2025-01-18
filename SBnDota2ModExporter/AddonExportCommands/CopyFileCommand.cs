using System.IO;
using System.Text;
using SBnDota2ModExporter.GUI;

namespace SBnDota2ModExporter.AddonExportCommands;

public class CopyFileCommand(string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress, string pathToFile)
{
  public void Execute()
  {
    progress.Report(new AddonExportProgress(BuildInitialProgressMessage()));

    var fileInfo = new FileInfo(pathToFile);
    if (fileInfo.Exists is false)
    {
      progress.Report(new AddonExportProgress("File not exist:" +
                                              $"'{pathToFile}'{Environment.NewLine}" +
                                              Constants.SKIP_COMMAND_TEXT,
        Constants.RTB_FOREGROUND_COLOR_WARNING));

      return;
    }

    fileInfo.CopyTo(Path.Combine(addonOutputDirectoryFullPath, fileInfo.Name), true);

    progress.Report(new AddonExportProgress("File copying finished.", Constants.RTB_FOREGROUND_COLOR_SUCCESS));
  }

  private string BuildInitialProgressMessage()
  {
    var sb = new StringBuilder();
    sb.Append("Attempting to copy file:");
    sb.AppendLine();

    sb.Append($"'{pathToFile}'");
    sb.AppendLine();

    sb.Append("into:");
    sb.AppendLine();

    sb.Append($"'{addonOutputDirectoryFullPath}'");
    sb.Append('.');

    return sb.ToString();
  }
}