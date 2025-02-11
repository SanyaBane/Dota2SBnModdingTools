using System.IO;
using System.Windows;
using CommonLib;
using VsndevtsEditor.Configs;
using VsndevtsEditor.GUI.MainWindow.ViewModels;
using VsndevtsEditor.GUI.MainWindow.Views;
using Serilog;

namespace VsndevtsEditor;

public partial class App
{
  #region Fields

  private MainControlViewModel? _mainControlViewModel;

  #endregion // Fields

  #region Ctor

  public App()
  {
    const string logDirectory = "VsndevtsEditor_Logs";

    string logFile = Path.Combine(logDirectory, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log");

    Logger = new LoggerConfiguration()
      .WriteTo.File(logFile, rollingInterval: RollingInterval.Day)
      .CreateLogger();

    AppDomain.CurrentDomain.UnhandledException += (s, e) =>
    {
      if (e.ExceptionObject is Exception ex)
      {
        Logger.Fatal(ex, "Unhandled Exception");
        Log.CloseAndFlush(); // Ensure logs are written before crash
      }
    };

    DispatcherUnhandledException += (s, e) =>
    {
      Logger.Fatal(e.Exception, "Dispatcher Unhandled Exception");
      Log.CloseAndFlush(); // Ensure logs are written before crash
      e.Handled = true;
    };
  }

  #endregion // Ctor

  #region Properties

  public static ILogger Logger { get; private set; } = null!;

  #endregion // Properties

  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    if (!EnsureTemplateDirectoriesExists())
      Application.Current.Shutdown();

    Application.Current.Exit += Application_OnExit;

    _mainControlViewModel = new MainControlViewModel();

    var mainWindow = new MainWindowView
    {
      DataContext = _mainControlViewModel
    };

    mainWindow.ShowDialog();
  }

  private void Application_OnExit(object sender, ExitEventArgs e)
  {
    Log.CloseAndFlush();
  }

  private bool EnsureTemplateDirectoriesExists()
  {
    var templateDirectoriesSettingsFileInfo = new FileInfo(Constants.PathToTemplateDirectoriesSettingsFile);
    if (!templateDirectoriesSettingsFileInfo.Exists)
    {
      MessageBox.Show($"File '{Constants.PathToTemplateDirectoriesSettingsFile}' does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

      return false;
    }

    try
    {
      var templateDirectoriesSettings = XmlSerializerService.DeserilazeFromXml<TemplateDirectoriesSettings>(Constants.PathToTemplateDirectoriesSettingsFile);
      GlobalManager.Instance.TemplateDirectoriesSettings = templateDirectoriesSettings;
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

      return false;
    }

    var templateDirectoriesDirInfo = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Template_Directories"));
    if (!templateDirectoriesDirInfo.Exists)
      templateDirectoriesDirInfo.Create();

    var createdTemplateDirectoriesNames = new HashSet<string>();
    foreach (var templateDirectory in GlobalManager.Instance.TemplateDirectoriesSettings.TemplateDirectories)
    {
      var templateDir = new DirectoryInfo(Path.Combine(templateDirectoriesDirInfo.FullName, templateDirectory.DirectoryName));
      if (!templateDir.Exists)
      {
        templateDir.Create();
        createdTemplateDirectoriesNames.Add(templateDirectory.DirectoryName);
      }
    }

    if (createdTemplateDirectoriesNames.Count > 0)
    {
      var str = string.Join(Environment.NewLine, createdTemplateDirectoriesNames);
      MessageBox.Show($"Inside '{templateDirectoriesDirInfo.FullName}'{Environment.NewLine}" +
                      $"created following template directories:{Environment.NewLine}{Environment.NewLine}" +
                      $"{str}",
        "Error", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    GlobalManager.Instance.TemplateDirectoriesSettings.FillTemplateDirectoriesFileInfos();

    return true;
  }
}