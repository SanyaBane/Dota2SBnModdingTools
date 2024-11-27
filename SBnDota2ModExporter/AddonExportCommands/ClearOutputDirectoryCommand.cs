using System.IO;
using SBnDota2ModExporter.GUI;

namespace SBnDota2ModExporter.AddonExportCommands;

public static class ClearOutputDirectoryCommand
{
  public static void Execute(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress)
  {
    progress.Report(new AddonExportProgress("Attempting to clear addon output directory."));

    var addonOutputDirectory = new DirectoryInfo(addonOutputDirectoryFullPath);
    if (addonOutputDirectory.Exists is false)
    {
      progress.Report(new AddonExportProgress($"Addon addon output directory not exist:{Environment.NewLine}" +
                                              $"'{addonOutputDirectory.FullName}'{Environment.NewLine}" +
                                              Constants.SKIP_COMMAND_TEXT,
        Constants.RTB_FOREGROUND_COLOR_WARNING));

      return;
    }

    foreach (FileInfo file in addonOutputDirectory.GetFiles())
      file.Delete();

    foreach (DirectoryInfo dir in addonOutputDirectory.GetDirectories())
      dir.Delete(true);

    progress.Report(new AddonExportProgress($"Content of directory '{addonOutputDirectory.FullName}' is deleted.", Constants.RTB_FOREGROUND_COLOR_SUCCESS));
  }
}