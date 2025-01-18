using System.Diagnostics;
using System.IO;
using SBnDota2ModExporter.GUI;

namespace SBnDota2ModExporter.AddonExportCommands;

public class CompileAddonCommand(string dota2AddonName, IProgress<AddonExportProgress> progress)
{
  public async Task Execute()
  {
    progress.Report(new AddonExportProgress("Attempting to compile addon..."));

    var addonContentDirectoryFullPath = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsContentDirectoryInfo.FullName, dota2AddonName);
    var addonContentDirectoryInfo = new DirectoryInfo(addonContentDirectoryFullPath);

    if (!addonContentDirectoryInfo.Exists)
    {
      progress.Report(new AddonExportProgress($"Addon content directory not exist:{Environment.NewLine}" +
                                               $"'{addonContentDirectoryInfo.FullName}'{Environment.NewLine}" +
                                               Constants.SKIP_COMMAND_TEXT,
        Constants.RTB_FOREGROUND_COLOR_WARNING));

      return;
    }

    var arguments = $"-r \"{Path.Combine(addonContentDirectoryInfo.FullName, "*.*")}\"";
    var processStartInfo = new ProcessStartInfo(GlobalManager.Instance.Dota2GameMainInfo.ResourceCompilerExecutableFileInfo.FullName)
    {
      Arguments = arguments,
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

      progress.Report(new AddonExportProgress(args.Data, Constants.RTB_FOREGROUND_COLOR_ANOTHER_PROGRAM));
      Console.WriteLine($"Output: {args.Data}");
    };

    process.ErrorDataReceived += (sender, args) =>
    {
      if (string.IsNullOrEmpty(args.Data))
        return;

      progress.Report(new AddonExportProgress(args.Data, Constants.RTB_FOREGROUND_COLOR_ANOTHER_PROGRAM));
      Console.WriteLine($"Error: {args.Data}");
    };

    process.Start();
    process.BeginOutputReadLine();
    process.BeginErrorReadLine();

    await process.WaitForExitAsync();

    progress.Report(new AddonExportProgress("Addon compilation finished.", Constants.RTB_FOREGROUND_COLOR_SUCCESS));
  }
}