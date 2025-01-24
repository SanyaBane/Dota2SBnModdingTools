using System.IO;
using System.Windows;
using CommonLib;
using VsndevtsEditor.Configs;
using VsndevtsEditor.GUI.MainWindow.ViewModels;
using VsndevtsEditor.GUI.MainWindow.Views;

namespace VsndevtsEditor;

public partial class App
{
  #region Fields

  private MainControlViewModel? _mainControlViewModel;

  #endregion // Fields

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