using System.IO;
using System.Windows;
using Common.WPF;
using Microsoft.Win32;
using SBnDota2ModExporter.Configs;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.Data;

namespace SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy;

public class DestinationOfCopyCreateUpdateViewModel : BaseViewModel
{
  #region Fields

  private readonly string _dota2AddonName;
  private readonly AddonExportOutputInfoViewModel _addonExportOutputInfoViewModelVm;

  private bool _isDirty;
  private BaseDestinationOfCopyDataViewModel _destinationOfCopyDataViewModel;
  private enDestinationOfCopyMode _selectedDestinationOfCopyMode;

  private readonly Dictionary<enDestinationOfCopyMode, BaseDestinationOfCopyDataViewModel> _createdViewModels = new();

  #endregion // Fields

  #region Ctor

  public DestinationOfCopyCreateUpdateViewModel(string dota2AddonName, AddonExportOutputInfoViewModel addonExportOutputInfoViewModelVm, string fullPath, enDestinationOfCopyMode destinationOfCopyMode)
  {
    _dota2AddonName = dota2AddonName;
    _addonExportOutputInfoViewModelVm = addonExportOutputInfoViewModelVm;
    _selectedDestinationOfCopyMode = destinationOfCopyMode;

    SetPathToDirectoryCommand = new DelegateCommand(ExecuteSetPathToDirectory);

    FullPathToDirectoryVm = new FullPathToDirectoryViewModel
    {
      FullPath = fullPath
    };
    FullPathToDirectoryVm.FullPathChange += OnFullPathChange;

    var instance = CreateDestinationOfCopyDataViewModelInstance(_selectedDestinationOfCopyMode);
    _createdViewModels[_selectedDestinationOfCopyMode] = instance;

    _destinationOfCopyDataViewModel = instance;
    SubscribeToDestinationOfCopyDataVmChanges();
  }

  #endregion // Ctor

  #region Events

  public event Action? IsDirtyChange;

  #endregion // Events

  #region Properties

  public FullPathToDirectoryViewModel FullPathToDirectoryVm { get; }

  public bool IsDirty
  {
    get => _isDirty;
    set
    {
      _isDirty = value;
      OnPropertyChanged();

      IsDirtyChange?.Invoke();
    }
  }

  public BaseDestinationOfCopyDataViewModel DestinationOfCopyDataViewModel
  {
    get => _destinationOfCopyDataViewModel;
    private set
    {
      _destinationOfCopyDataViewModel = value;
      OnPropertyChanged();
    }
  }

  public enDestinationOfCopyMode SelectedDestinationOfCopyMode
  {
    get => _selectedDestinationOfCopyMode;
    set
    {
      _selectedDestinationOfCopyMode = value;
      OnPropertyChanged();

      UpdateSelectionViewModel();
      DestinationOfCopyDataViewModel.PreviewOutputPathViewModel.UpdateFullPath(_dota2AddonName, _addonExportOutputInfoViewModelVm);

      IsDirty = true;
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
    if (!string.IsNullOrEmpty(FullPathToDirectoryVm.FullPath))
    {
      directoryInfo = new DirectoryInfo(FullPathToDirectoryVm.FullPath);
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

      FullPathToDirectoryVm.FullPath = openFolderDialog.FolderName;
    }
  }

  #endregion // Command Execute Handlers

  #region Event Handlers

  private void OnFullPathChange()
  {
    DestinationOfCopyDataViewModel.PreviewOutputPathViewModel.UpdateFullPath(_dota2AddonName, _addonExportOutputInfoViewModelVm);
    IsDirty = true;
  }

  private void DestinationOfCopyDataViewModel_OnIsDirtyChange()
  {
    IsDirty = true;
  }

  #endregion // Event Handlers
  
  #region Public Methods

  public bool IsValidViewModel()
  {
    return FullPathToDirectoryVm.IsValidViewModel() && DestinationOfCopyDataViewModel.IsValidViewModel();
  }

  public DestinationOfCopyCreateUpdateViewModel Clone()
  {
    return new DestinationOfCopyCreateUpdateViewModel(_dota2AddonName, _addonExportOutputInfoViewModelVm.Clone(), FullPathToDirectoryVm.FullPath, SelectedDestinationOfCopyMode)
    {
      // FullPathToDirectoryVm =
      // {
      //   FullPath = FullPathToDirectoryVm.FullPath
      // },
      // SelectedDestinationOfCopyMode = SelectedDestinationOfCopyMode,
      DestinationOfCopyDataViewModel = DestinationOfCopyDataViewModel.Clone(),
    };
  }

  #endregion // Public Methods

  #region Protected Methods

  protected override void OnTokenChanged()
  {
    base.OnTokenChanged();

    foreach (var baseDestinationOfCopyDataViewModel in _createdViewModels)
    {
      baseDestinationOfCopyDataViewModel.Value.Token = Token;
    }
  }

  #endregion // Protected Methods

  #region Private Methods

  private void UpdateSelectionViewModel()
  {
    UnsubscribeFromDestinationOfCopyDataVmChanges();

    if (_createdViewModels.TryGetValue(_selectedDestinationOfCopyMode, out var viewModel))
    {
      DestinationOfCopyDataViewModel = viewModel;
    }
    else
    {
      var instance = CreateDestinationOfCopyDataViewModelInstance(_selectedDestinationOfCopyMode);
      _createdViewModels[_selectedDestinationOfCopyMode] = instance;

      DestinationOfCopyDataViewModel = instance;
    }

    SubscribeToDestinationOfCopyDataVmChanges();
  }

  private void SubscribeToDestinationOfCopyDataVmChanges()
  {
    DestinationOfCopyDataViewModel.IsDirtyChange += DestinationOfCopyDataViewModel_OnIsDirtyChange;
  }

  private void UnsubscribeFromDestinationOfCopyDataVmChanges()
  {
    DestinationOfCopyDataViewModel.IsDirtyChange -= DestinationOfCopyDataViewModel_OnIsDirtyChange;
  }

  private BaseDestinationOfCopyDataViewModel CreateDestinationOfCopyDataViewModelInstance(enDestinationOfCopyMode destinationOfCopyMode)
  {
    switch (destinationOfCopyMode)
    {
      case enDestinationOfCopyMode.CopyToRoot:
        return new DefaultDestinationOfCopyDataViewModel(() => FullPathToDirectoryVm.FullPath)
        {
          Token = Token,
        };
      case enDestinationOfCopyMode.CopyToRootUsingRelativePaths:
        return new RelativeDestinationOfCopyDataViewModel(() => FullPathToDirectoryVm.FullPath)
        {
          Token = Token,
        };
      case enDestinationOfCopyMode.CopyToSpecifiedDirectory:
        return new SpecifiedDestinationOfCopyDataViewModel(() => FullPathToDirectoryVm.FullPath, _addonExportOutputInfoViewModelVm, _dota2AddonName)
        {
          Token = Token,
        };
      default:
        throw new ArgumentOutOfRangeException(nameof(destinationOfCopyMode), destinationOfCopyMode, null);
    }
  }

  #endregion // Private Methods
}