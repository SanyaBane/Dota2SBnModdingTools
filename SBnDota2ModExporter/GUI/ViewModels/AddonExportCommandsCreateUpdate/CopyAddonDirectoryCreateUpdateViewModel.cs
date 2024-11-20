using System.IO;
using System.Windows;
using Common.WPF;
using Microsoft.Win32;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

public class CopyAddonDirectoryCreateUpdateViewModel : BaseViewModel, IAddonExportCommandCreateUpdateViewModel
{
  #region Fields

  private readonly string _dota2AddonName;
  private readonly Action<bool>? _canExecuteOkCommandCallback;

  private string _pathToDirectory = string.Empty;
  private bool _isCopySubfolders = true;
  private bool _isDirty;

  #endregion // Fields

  #region Ctor

  public CopyAddonDirectoryCreateUpdateViewModel(string dota2AddonName, Action<bool>? canExecuteOkCommandCallback, CopyAddonDirectoryViewModel? editVm)
  {
    _dota2AddonName = dota2AddonName;
    _canExecuteOkCommandCallback = canExecuteOkCommandCallback;

    SetPathToDirectoryCommand = new DelegateCommand(ExecuteSetPathToDirectory);

    if (editVm != null)
    {
      _pathToDirectory = editVm.PathToAddonDirectory;
      UpdateVmAfterPathToDirectoryChange();

      _isCopySubfolders = editVm.IsCopySubfolders;
    }
  }

  #endregion // Ctor

  #region Properties

  public string PathToDirectory
  {
    get => _pathToDirectory;
    set
    {
      _pathToDirectory = value;
      OnPropertyChanged();

      UpdateVmAfterPathToDirectoryChange();

      IsDirty = true;
    }
  }

  public string? PreviewResult { get; private set; }

  public bool IsCopySubfolders
  {
    get => _isCopySubfolders;
    set
    {
      _isCopySubfolders = value;
      OnPropertyChanged();

      IsDirty = true;
    }
  }

  public bool IsDirty
  {
    get => _isDirty;
    set
    {
      _isDirty = value;
      OnPropertyChanged();

      RefreshCommands();
    }
  }

  #endregion // Properties

  #region Commands

  public DelegateCommand SetPathToDirectoryCommand { get; }

  #endregion // Commands

  #region Command Execute Handlers

  private void ExecuteSetPathToDirectory(object obj)
  {
    var addonDirectoryFullPath = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, _dota2AddonName);
    var addonDirectoryInfo = new DirectoryInfo(addonDirectoryFullPath);

    DirectoryInfo? directoryInfo = null;
    if (!string.IsNullOrEmpty(PathToDirectory))
    {
      directoryInfo = new DirectoryInfo(PathToDirectory);
    }

    string? initialDirectory = null;
    if (directoryInfo is { Exists: true })
    {
      initialDirectory = directoryInfo.FullName;
    }

    var openFolderDialog = new OpenFolderDialog()
    {
      InitialDirectory = initialDirectory ?? addonDirectoryInfo.FullName,
      AddToRecent = false,
    };

    if (openFolderDialog.ShowDialog() == true)
    {
      // check if pointed folder is exist inside of addon "game" folder
      if (openFolderDialog.FolderName.IndexOf(addonDirectoryInfo.FullName, StringComparison.InvariantCultureIgnoreCase) != 0)
      {
        MessageBox.Show($"You need to point to folder inside of your addon:{Environment.NewLine}" +
                        $"'{addonDirectoryInfo.FullName}'", 
          "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        
        return;
      }
      
      PathToDirectory = openFolderDialog.FolderName;
    }
  }

  #endregion // Command Execute Handlers

  #region Public Methods

  public override void RefreshCommands()
  {
    base.RefreshCommands();

    _canExecuteOkCommandCallback.Invoke(CanExecuteOkCommand());
  }

  #endregion // Public Methods

  #region Private Methods

  private bool CanExecuteOkCommand()
  {
    return IsDirty && !string.IsNullOrEmpty(PathToDirectory);
  }

  private void UpdateVmAfterPathToDirectoryChange()
  {
    if (string.IsNullOrEmpty(_pathToDirectory))
    {
      PreviewResult = string.Empty;
    }
    else
    {
      var dir = new DirectoryInfo(_pathToDirectory);
      PreviewResult = Path.Combine(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath, _dota2AddonName, dir.Name);
    }

    OnPropertyChanged(nameof(PreviewResult));
  }

  #endregion // Private Methods
}