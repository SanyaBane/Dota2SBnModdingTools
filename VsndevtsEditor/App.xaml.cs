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
    
    var foldersSettingsFileInfo = new FileInfo(_pathToFoldersSettingsFile);
    if (!foldersSettingsFileInfo.Exists)
    {
      MessageBox.Show($"File '{_pathToFoldersSettingsFile}' does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      
      Application.Current.Shutdown();
    }

    try
    {
      var folderSettings = XmlSerializerService.DeserilazeFromXml<FolderSettings>(_pathToFoldersSettingsFile);
      GlobalManager.Instance.FolderSettings = folderSettings;
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      
      Application.Current.Shutdown();
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
    

    var modExporterGlobalConfig = GlobalManager.Instance.LoadOrCreateConfigFile();

    string dota2ExeFullPath = modExporterGlobalConfig.Dota2ExeFullPath;

    var resultValidateDota2ExeFullPath = ValidateDota2ExeFullPathOnStartup(dota2ExeFullPath);
    if (resultValidateDota2ExeFullPath.IsFailure)
    {
      if (!string.IsNullOrEmpty(resultValidateDota2ExeFullPath.Error))
      {
        MessageBox.Show(resultValidateDota2ExeFullPath.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      Application.Current.Shutdown();
    }

    GlobalManager.Instance.GlobalSettings.Dota2ExeFullPath = resultValidateDota2ExeFullPath.Value;

    var resultTrySetFullPathToDota2Exe = GlobalManager.Instance.UpdateDota2GameMainInfo();
    if (resultTrySetFullPathToDota2Exe.IsFailure)
    {
      MessageBox.Show(resultTrySetFullPathToDota2Exe.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

      Application.Current.Shutdown();
    }

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
    GlobalManager.Instance.VsndevtsEditorGlobalConfig.Dota2ExeFullPath = GlobalManager.Instance.GlobalSettings.Dota2ExeFullPath;

    GlobalManager.Instance.TrySaveConfigFile();
  }

  private Result<string> ValidateDota2ExeFullPathOnStartup(string dota2ExeFullPath)
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