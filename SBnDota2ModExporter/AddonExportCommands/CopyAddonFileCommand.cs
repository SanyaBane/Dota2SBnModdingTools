using System.IO;
using System.Text;
using SBnDota2ModExporter.GUI;

namespace SBnDota2ModExporter.AddonExportCommands;

public class CopyAddonFileCommand(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress, string pathToAddonFile)
{
  public Task Execute()
  {
    throw new NotImplementedException();

    progress.Report(new AddonExportProgress(BuildInitialProgressMessage()));

    var fullPathToFile = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dota2AddonName, pathToAddonFile);
    var fileInfo = new FileInfo(fullPathToFile);
    if (fileInfo.Exists is false)
    {
      progress.Report(new AddonExportProgress("Addon file not exist:" +
                                              $"'{pathToAddonFile}'{Environment.NewLine}" +
                                              Constants.SKIP_COMMAND_TEXT,
        Constants.RTB_FOREGROUND_COLOR_WARNING));

      return Task.CompletedTask;
    }

    fileInfo.CopyTo(Path.Combine(addonOutputDirectoryFullPath, fileInfo.Name), true);

    progress.Report(new AddonExportProgress("Addon file copying finished.", Constants.RTB_FOREGROUND_COLOR_SUCCESS));
  }

  private string BuildInitialProgressMessage()
  {
    var sb = new StringBuilder();
    sb.Append("Attempting to copy addon file:");
    sb.AppendLine();

    sb.Append($"'{pathToAddonFile}'");
    sb.AppendLine();

    sb.Append("into:");
    sb.AppendLine();

    sb.Append($"'{addonOutputDirectoryFullPath}'");
    sb.Append('.');

    return sb.ToString();
  }
}