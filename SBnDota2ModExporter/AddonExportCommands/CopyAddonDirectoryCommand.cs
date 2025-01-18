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
  public Task Execute()
  {
    progress.Report(new AddonExportProgress(BuildInitialProgressMessage(selectedDestinationOfCopyMode)));

    try
    {
      var fullPathToAddonDirectory = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dota2AddonName, pathToAddonDirectory);
      var addonDirectoryInfo = new DirectoryInfo(fullPathToAddonDirectory);
      if (addonDirectoryInfo.Exists is false)
      {
        progress.Report(new AddonExportProgress($"Addon directory not exist:{Environment.NewLine}" +
                                                $"'{pathToAddonDirectory}'{Environment.NewLine}" +
                                                Constants.SKIP_COMMAND_TEXT,
          Constants.RTB_FOREGROUND_COLOR_WARNING));

        return Task.CompletedTask;
      }

      switch (selectedDestinationOfCopyMode)
      {
        case enDestinationOfCopyMode.CopyToRoot:
        {
          CopyToRoot(addonDirectoryInfo);
          break;
        }
        case enDestinationOfCopyMode.CopyToRootUsingRelativePaths:
        {
          CopyToRootUsingRelativePaths(addonDirectoryInfo);
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

    return Task.CompletedTask;
  }

  private void CopyToRoot(DirectoryInfo addonDirectoryInfo)
  {
    var outputDirectoryInfo = new DirectoryInfo(addonOutputDirectoryFullPath);
    FileManager.CopyDirectory(addonDirectoryInfo, outputDirectoryInfo, isCopySubfolders);
  }
  
  private void CopyToRootUsingRelativePaths(DirectoryInfo addonDirectoryInfo)
  {
    var addonGameDirectoryFullPath = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dota2AddonName);
    if (!addonDirectoryInfo.FullName.StartsWith(addonGameDirectoryFullPath, StringComparison.InvariantCultureIgnoreCase))
      throw new Exception();

    var newPath = addonDirectoryInfo.FullName.Substring(addonGameDirectoryFullPath.Length);

    if (newPath[0] == '\\')
      newPath = newPath.Substring(1);

    var OutputFullPath = Path.Combine(addonOutputDirectoryFullPath, newPath);

    FileManager.CopyDirectoryContent(addonDirectoryInfo, new DirectoryInfo(OutputFullPath), isCopySubfolders);
  }

  private string BuildInitialProgressMessage(enDestinationOfCopyMode enDestinationOfCopyMode)
  {
    var sb = new StringBuilder();
    sb.Append("Attempting to copy addon directory");

    if (isCopySubfolders)
      sb.Append(" (with subdirectories)");

    sb.Append('.');
    // sb.Append(':');
    // sb.AppendLine();
    //
    // sb.Append($"'{pathToAddonDirectory}'");
    // sb.AppendLine();
    //
    // sb.Append("into:");
    // sb.AppendLine();
    //
    // switch (selectedDestinationOfCopyMode)
    // {
    //   
    // }
    //
    // sb.Append($"'{addonOutputDirectoryFullPath}'");
    // sb.Append('.');

    return sb.ToString();
  }
}