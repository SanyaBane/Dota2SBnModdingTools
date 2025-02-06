using System.Windows;
using CommonLib;
using CSharpFunctionalExtensions;
using SBnDota2ModExporter.Configs.Main;
using SBnDota2ModExporter.GUI.ViewModels;
using SBnDota2ModExporter.GUI.Views;

namespace SBnDota2ModExporter;

public partial class App
{
  private MainControlViewModel? _mainControlViewModel;

  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    var config = GlobalManager.Instance.EnsureConfigFileExists();

    string dota2ExeFullPath = config.Dota2ExeFullPath;

    var resultValidateDota2ExeFullPath = ValidateDota2ExeFullPathOnStartup(dota2ExeFullPath);
    if (resultValidateDota2ExeFullPath.IsFailure || string.IsNullOrEmpty(resultValidateDota2ExeFullPath.Value))
    {
      if (resultValidateDota2ExeFullPath.IsFailure
          && !string.IsNullOrEmpty(resultValidateDota2ExeFullPath.Error))
      {
        MessageBox.Show(resultValidateDota2ExeFullPath.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      Application.Current.Shutdown();
      return;
    }

    GlobalManager.Instance.GlobalSettings.Dota2ExeFullPath = resultValidateDota2ExeFullPath.Value;
    GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath = config.OutputDirectoryFullPath;

    var resultTrySetFullPathToDota2Exe = GlobalManager.Instance.UpdateDota2GameMainInfo();
    if (resultTrySetFullPathToDota2Exe.IsFailure)
    {
      MessageBox.Show(resultTrySetFullPathToDota2Exe.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

      Application.Current.Shutdown();
      return;
    }

    Application.Current.Exit += Application_OnExit;

    _mainControlViewModel = new MainControlViewModel(config.AddonExporterShortConfigs);

    var mainWindow = new MainWindowView
    {
      DataContext = _mainControlViewModel
    };

    mainWindow.ShowDialog();
  }

  private void Application_OnExit(object sender, ExitEventArgs e)
  {
    var addonExporterShortConfigs = new List<AddonExporterShortConfig>();
    foreach (var loadedAddonExporterInfoViewModel in _mainControlViewModel.AddonExporterInfoViewModels)
    {
      if (loadedAddonExporterInfoViewModel.AddonConfigFileInfo == null)
        continue;

      addonExporterShortConfigs.Add(new AddonExporterShortConfig()
      {
        FileFullPath = loadedAddonExporterInfoViewModel.AddonConfigFileInfo.FullName,
        IsChecked = loadedAddonExporterInfoViewModel.IsChecked
      });
    }

    GlobalManager.Instance.ModExporterGlobalConfig.AddonExporterShortConfigs = addonExporterShortConfigs;
    GlobalManager.Instance.ModExporterGlobalConfig.Dota2ExeFullPath = GlobalManager.Instance.GlobalSettings.Dota2ExeFullPath;
    GlobalManager.Instance.ModExporterGlobalConfig.OutputDirectoryFullPath = _mainControlViewModel.OutputDirectoryFullPath;

    GlobalManager.Instance.TrySaveConfigFile();
  }

  private Result<string?> ValidateDota2ExeFullPathOnStartup(string dota2ExeFullPath)
  {
    if (string.IsNullOrEmpty(dota2ExeFullPath))
    {
      MessageBox.Show($"Select 'dota2.exe' file{Environment.NewLine}" +
                      $"Example: C:\\Program Files\\Steam\\steamapps\\common\\dota 2 beta\\game\\bin\\win64\\dota2.exe",
        "SBnDota2ModExporter startup", MessageBoxButton.OK, MessageBoxImage.Exclamation);

      var resultCallDialogSetDota2ExePath = GlobalManager.Instance.CallDialogSetDota2ExePath();
      return resultCallDialogSetDota2ExePath;
    }

    return dota2ExeFullPath;
  }
}