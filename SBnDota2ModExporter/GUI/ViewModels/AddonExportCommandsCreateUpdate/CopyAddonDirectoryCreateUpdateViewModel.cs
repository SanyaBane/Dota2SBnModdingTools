using System.IO;
using System.Windows;
using Common.WPF;
using Microsoft.Win32;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

public class CopyAddonDirectoryCreateUpdateViewModel : BaseViewModel, IAddonExportCommandCreateUpdateViewModel
{
  #region Fields

  private DestinationOfCopyViewModel _destinationOfCopy = new();
  private readonly string _dota2AddonName;
  private readonly Action<bool>? _canExecuteOkCommandCallback;

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
      PathToDirectory.FullPath = editVm.PathToAddonDirectory;

      _isCopySubfolders = editVm.IsCopySubfolders;
      DestinationOfCopy = editVm.DestinationOfCopy.Clone();
      
      DestinationOfCopy.UpdateVmAfterPathToDirectoryChange(PathToDirectory, _dota2AddonName);
    }

    PathToDirectory.FullPathChange += () =>
    {
      DestinationOfCopy.UpdateVmAfterPathToDirectoryChange(PathToDirectory, _dota2AddonName);

      IsDirty = true;
    };

    _destinationOfCopy.SelectedDestinationOfCopyChange += () =>
    {
      DestinationOfCopy.UpdateVmAfterPathToDirectoryChange(PathToDirectory, _dota2AddonName);

      IsDirty = true;
    };
  }

  #endregion // Ctor

  #region Properties

  public DestinationOfCopyViewModel DestinationOfCopy
  {
    get => _destinationOfCopy;
    set
    {
      _destinationOfCopy = value;
      OnPropertyChanged();
    }
  }

  public PathToDirectoryViewModel PathToDirectory { get; } = new PathToDirectoryViewModel();

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
    if (!string.IsNullOrEmpty(PathToDirectory.FullPath))
    {
      directoryInfo = new DirectoryInfo(PathToDirectory.FullPath);
    }

    string? initialDirectory = null;
    if (directoryInfo is { Exists: true })
    {
      initialDirectory = directoryInfo.FullName;
    }

    var openFolderDialog = new OpenFolderDialog()
    {
      AddToRecent = false,
    };

    if (initialDirectory != null)
    {
      openFolderDialog.InitialDirectory = initialDirectory;
    }
    else
    {
      openFolderDialog.InitialDirectory = addonDirectoryInfo.FullName;
      
      var initDirInfo = new DirectoryInfo(openFolderDialog.InitialDirectory);
      if (initDirInfo.Exists is false)
      {
        MessageBox.Show($"Can not find addon 'game' directory:{Environment.NewLine}" +
                        $"'{openFolderDialog.InitialDirectory}'{Environment.NewLine}{Environment.NewLine}" +
                        $"Maybe it was not compiled yet? It's would be much easier if you first compile addon and only then use this window to point to already compiled directory which must be copied to output directory.", 
          "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
      }
    }
    
    if (openFolderDialog.ShowDialog() == true)
    {
      // check if pointed folder is exist inside of addon "game" folder
      if (openFolderDialog.FolderName.IndexOf(addonDirectoryInfo.FullName, StringComparison.InvariantCultureIgnoreCase) != 0)
      {
        MessageBox.Show($"You need to point to directory inside of your addon 'game' directory:{Environment.NewLine}" +
                        $"'{addonDirectoryInfo.FullName}'",
          "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        return;
      }

      PathToDirectory.FullPath = openFolderDialog.FolderName;
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
    return IsDirty && !string.IsNullOrEmpty(PathToDirectory.FullPath);
  }

  #endregion // Private Methods
}