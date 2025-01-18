using System.IO;
using System.Text;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI;

namespace SBnDota2ModExporter.AddonExportCommands;

public class CopyAddonDirectoryCommand(
  string dota2AddonName,
  string addonOutputDirectoryFullPath,
  IProgress<AddonExportProgress> progress,
  string pathToAddonDirectory,
  enDestinationOfCopyMode selectedDestinationOfCopyMode,
  bool isCopySubfolders)
{
  public void Execute()
  {
    progress.Report(new AddonExportProgress(BuildInitialProgressMessage()));

    try
    {
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

      switch (selectedDestinationOfCopyMode)
      {
        case enDestinationOfCopyMode.CopyToRoot:
        {
          var outputDirectoryInfo = new DirectoryInfo(addonOutputDirectoryFullPath);
          FileManager.CopyDirectory(pathToDirectoryInfo, outputDirectoryInfo, isCopySubfolders);
          break;
        }
        case enDestinationOfCopyMode.CopyToRootUsingRelativePaths:
        {
          CopyToRootUsingRelativePaths(fullPathToDirectory, pathToDirectoryInfo);
          break;
        }
        case enDestinationOfCopyMode.CopyToSpecifiedDirectory:
        {
          throw new NotImplementedException();
        }
        default:
          throw new ArgumentOutOfRangeException(nameof(selectedDestinationOfCopyMode), selectedDestinationOfCopyMode, null);
      }

      progress.Report(new AddonExportProgress("Addon directory copying finished.", Constants.RTB_FOREGROUND_COLOR_SUCCESS));
    }
    catch (Exception ex)
    {
      progress.Report(new AddonExportProgress($"Error: {ex.ToString()}", Constants.RTB_FOREGROUND_COLOR_ERROR));
    }
  }

  private void CopyToRootUsingRelativePaths(string fullPathToDirectory, DirectoryInfo pathToDirectoryInfo)
  {
    var fullPathToDirectoryInfo = new DirectoryInfo(fullPathToDirectory);

    var addonGameDirectoryFullPath = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dota2AddonName);
    if (!fullPathToDirectoryInfo.FullName.StartsWith(addonGameDirectoryFullPath, StringComparison.InvariantCultureIgnoreCase))
      throw new Exception();

    var newPath = fullPathToDirectoryInfo.FullName.Substring(addonGameDirectoryFullPath.Length);

    if (newPath[0] == '\\')
      newPath = newPath.Substring(1);

    var OutputFullPath = Path.Combine(addonOutputDirectoryFullPath, newPath);

    FileManager.CopyDirectoryContent(pathToDirectoryInfo, new DirectoryInfo(OutputFullPath), isCopySubfolders);
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