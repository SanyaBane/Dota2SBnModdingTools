using System.IO;
using System.Windows;
using CommonLib;
using CSharpFunctionalExtensions;
using VsndevtsEditor.Configs;
using VsndevtsEditor.GUI.MainWindow.ViewModels;
using VsndevtsEditor.GUI.MainWindow.Views;

namespace VsndevtsEditor;

public partial class App
{
  #region Fields


  private readonly string _pathToFoldersSettingsFile = Path.Combine(Environment.CurrentDirectory, "Settings", "ActionFoldersSettings.xml");
  
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
    var foldersSettingsFileInfo = new FileInfo(_pathToFoldersSettingsFile);
    if (!foldersSettingsFileInfo.Exists)
    {
      MessageBox.Show($"File '{_pathToFoldersSettingsFile}' does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      
      return false;
    }

    try
    {
      var folderSettings = XmlSerializerService.DeserilazeFromXml<FolderSettings>(_pathToFoldersSettingsFile);
      GlobalManager.Instance.FolderSettings = folderSettings;
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
    foreach (var folderSettingsFolder in GlobalManager.Instance.FolderSettings.Folders)
    {
      var templateDir = new DirectoryInfo(Path.Combine(templateDirectoriesDirInfo.FullName, folderSettingsFolder.FolderName));
      if (!templateDir.Exists)
      {
        templateDir.Create();
        createdTemplateDirectoriesNames.Add(folderSettingsFolder.FolderName);
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

    return true;
  }
}