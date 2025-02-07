using System.IO;
using System.Windows;
using CSharpFunctionalExtensions;
using RemoveCosmetics.Constants;
using RemoveCosmetics.GUI.MainControl;
using RemoveCosmetics.GUI.MainWindow;
using RemoveCosmetics.Settings;
using Serilog;

namespace RemoveCosmetics;

public partial class App
{
  #region Fields

  private MainControlViewModel? _mainControlViewModel;

  #endregion // Fields

  #region Ctor

  public App()
  {
    const string logDirectory = "RemoveCosmetics Logs";
    Directory.CreateDirectory(logDirectory);

    string logFile = Path.Combine(logDirectory, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log");

    Logger = new LoggerConfiguration()
      .WriteTo.File(logFile, rollingInterval: RollingInterval.Day)
      .CreateLogger();

    AppDomain.CurrentDomain.UnhandledException += (s, e) =>
    {
      if (e.ExceptionObject is Exception ex)
        Log.Error(ex, "Unhandled Exception");
    };

    DispatcherUnhandledException += (s, e) =>
    {
      Log.Error(e.Exception, "Dispatcher Unhandled Exception");
      e.Handled = true;
    };
  }

  #endregion // Ctor

  #region Properties

  public static ILogger Logger { get; private set; } = null!;

  #endregion // Properties

  #region Event Handlers

  private void Application_OnExit(object sender, ExitEventArgs e)
  {
    if (SettingsManager.Instance.RemoveCosmeticsConfig.IsDirty)
    {
      var saveConfigFileResult = SettingsManager.Instance.TrySaveConfigFile();
      if (saveConfigFileResult.IsFailure)
      {
        Logger.Error(saveConfigFileResult.Error);
      }
    }
  }

  #endregion // Event Handlers

  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    var config = SettingsManager.Instance.EnsureConfigFileExists();

    var resultValidateDota2ExeFullPath = ValidateDota2ExeFullPathOnStartup(config.Dota2ExeFullPath);
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

    if (resultValidateDota2ExeFullPath.Value != config.Dota2ExeFullPath)
    {
      SettingsManager.Instance.RemoveCosmeticsConfig.Dota2ExeFullPath = resultValidateDota2ExeFullPath.Value;
    }

    var updateDota2GameMainInfoResult = SettingsManager.Instance.UpdateDota2GameMainInfo();
    if (updateDota2GameMainInfoResult.IsFailure)
    {
      MessageBox.Show(updateDota2GameMainInfoResult.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

      Application.Current.Shutdown();
      return;
    }

    _mainControlViewModel = new MainControlViewModel(SettingsManager.Instance.Dota2GameMainInfo);
    _mainControlViewModel.InitializeViewModel();

    Application.Current.Exit += Application_OnExit;

    var mainWindow = new MainWindowView
    {
      DataContext = _mainControlViewModel
    };

    mainWindow.ShowDialog();
  }

  private Result<string?> ValidateDota2ExeFullPathOnStartup(string dota2ExeFullPath)
  {
    if (string.IsNullOrEmpty(dota2ExeFullPath))
    {
      MessageBox.Show($"Select 'dota2.exe' file{Environment.NewLine}" +
                      $"Example: C:\\Program Files\\Steam\\steamapps\\common\\dota 2 beta\\game\\bin\\win64\\dota2.exe",
        $"{Constants_General.PROGRAM_TITLE} startup", MessageBoxButton.OK, MessageBoxImage.Exclamation);

      var resultCallDialogSetDota2ExePath = SettingsManager.Instance.CallDialogSetDota2ExePath();
      return resultCallDialogSetDota2ExePath;
    }

    return dota2ExeFullPath;
  }
}