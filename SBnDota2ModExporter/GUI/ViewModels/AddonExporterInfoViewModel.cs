using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Common.WPF;
using Microsoft.Win32;
using SBnDota2ModExporter.Configs.AddonsExporter;
using SBnDota2ModExporter.Factories;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.ViewModels;

public class AddonExporterInfoViewModel : BaseViewModel
{
  #region Fields

  private FileInfo? _addonConfigFileInfo;
  private string _dota2AddonName = string.Empty;
  private string _addonOutputDirectory = string.Empty;
  private bool _isChecked;
  private bool _isDirty;
  private ICollectionView _addonExportCommandViewModelsCollectionView;

  private IAddonExportCommandViewModel? _selectedAddonExportCommandViewModel;

  private bool _canExecuteExportAddonCommand;

  #endregion // Fields

  #region Ctor

  public AddonExporterInfoViewModel()
  {
    SpecifyDota2AddonCommand = new DelegateCommand(ExecuteSpecifyDota2Addon);
    SaveCommand = new DelegateCommand(ExecuteSave, CanExecuteSave);
    SaveAsCommand = new DelegateCommand(ExecuteSaveAs);
    MoveUpCommand = new DelegateCommand(ExecuteMoveUp, CanExecuteMoveUp);
    MoveDownCommand = new DelegateCommand(ExecuteMoveDown, CanExecuteMoveDown);
    ExportCommandCreateCommand = new DelegateCommand(ExecuteExportCommandCreate);
    ExportCommandEditCommand = new DelegateCommand(ExecuteExportCommandEdit, CanExecuteExportCommandEdit);
    ExportCommandDeleteCommand = new DelegateCommand(ExecuteExportCommandDelete, CanExecuteExportCommandDelete);
    ExportCommandLoadDefaultTemplateCommand = new DelegateCommand(ExecuteExportCommandLoadDefaultTemplate);
    ExportAddonCommand = new DelegateCommand(ExecuteExportAddon, CanExecuteExportAddon);

    AddonExportCommandViewModels = new ObservableCollection<IAddonExportCommandViewModel>();
    AddonExportCommandViewModels.CollectionChanged += AddonExportCommandViewModelsOnCollectionChanged;

    GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPathChange += GlobalSettings_OnOutputDirectoryFullPathChange;

    UpdateAddonOutputDirectory();
    // _dota2AddonInfo = new Dota2AddonInfo(GlobalManager.Instance.Dota2GameMainInfo);
  }

  private void AddonExportCommandViewModelsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    if (e.NewItems != null)
      foreach (var newItem in e.NewItems)
      {
        var addonExportCommandViewModel = (IAddonExportCommandViewModel)newItem;
        addonExportCommandViewModel.IsCheckedChange += AddonExportCommandViewModelOnIsCheckedChange;
      }

    if (e.OldItems != null)
      foreach (var oldItem in e.OldItems)
      {
        var addonExportCommandViewModel = (IAddonExportCommandViewModel)oldItem;
        addonExportCommandViewModel.IsCheckedChange -= AddonExportCommandViewModelOnIsCheckedChange;
      }
  }

  private void AddonExportCommandViewModelOnIsCheckedChange()
  {
    IsDirty = true;
  }

  #endregion // Ctor

  #region Events

  public event Action? IsCheckedChange;
  public event Action? CanExecuteExportAddonCommandChange;
  public event Action<AddonExporterInfoViewModel>? ExportAddonExecute;

  #endregion // Events

  #region Properties

  public FileInfo? AddonConfigFileInfo
  {
    get => _addonConfigFileInfo;
    set
    {
      _addonConfigFileInfo = value;
      OnPropertyChanged();

      OnPropertyChanged(nameof(IsSaved));
      OnPropertyChanged(nameof(AddonConfigFileName));
    }
  }

  public string AddonConfigFileName => AddonConfigFileInfo == null ? string.Empty : Path.GetFileNameWithoutExtension(AddonConfigFileInfo.Name);

  public bool IsSaved => AddonConfigFileInfo != null;

  public string Dota2AddonName
  {
    get => _dota2AddonName;
    set
    {
      _dota2AddonName = value;
      OnPropertyChanged();

      UpdateAddonOutputDirectory();

      RefreshCommands();

      IsDirty = true;
    }
  }

  public string AddonOutputDirectory
  {
    get => _addonOutputDirectory;
    private set
    {
      _addonOutputDirectory = value;
      OnPropertyChanged();
    }
  }

  public bool IsChecked
  {
    get => _isChecked;
    set
    {
      _isChecked = value;
      OnPropertyChanged();

      IsCheckedChange?.Invoke();

      RefreshCommands();
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

  public ObservableCollection<IAddonExportCommandViewModel> AddonExportCommandViewModels { get; }

  public IAddonExportCommandViewModel? SelectedAddonExportCommandViewModel
  {
    get => _selectedAddonExportCommandViewModel;
    set
    {
      _selectedAddonExportCommandViewModel = value;
      OnPropertyChanged();

      RefreshCommands();
    }
  }

  public bool CanExecuteExportAddonCommand
  {
    get => _canExecuteExportAddonCommand;
    private set
    {
      _canExecuteExportAddonCommand = value;
      OnPropertyChanged();

      CanExecuteExportAddonCommandChange?.Invoke();
    }
  }

  #endregion // Properties

  #region Events

  public event Action? ClickExecuteExportCommandCreate;
  public event Action<IAddonExportCommandViewModel>? ClickExecuteExportCommandEdit;
  public event Action<IAddonExportCommandViewModel>? ItemStateUpdated;

  #endregion // Events

  #region Commands

  public DelegateCommand SpecifyDota2AddonCommand { get; }
  public DelegateCommand SaveCommand { get; }
  public DelegateCommand SaveAsCommand { get; }

  public DelegateCommand MoveUpCommand { get; }
  public DelegateCommand MoveDownCommand { get; }

  public DelegateCommand ExportCommandCreateCommand { get; }
  public DelegateCommand ExportCommandEditCommand { get; }
  public DelegateCommand ExportCommandDeleteCommand { get; }
  public DelegateCommand ExportCommandLoadDefaultTemplateCommand { get; }

  public DelegateCommand ExportAddonCommand { get; }

  #endregion // Commands

  #region Command Can Execute Handlers

  private bool CanExecuteSave(object obj)
  {
    return IsDirty && AddonConfigFileInfo is { Exists: true };
  }

  private bool CanExecuteMoveUp(object obj)
  {
    if (SelectedAddonExportCommandViewModel == null)
      return false;

    var index = AddonExportCommandViewModels.IndexOf(SelectedAddonExportCommandViewModel);
    if (index <= 0)
      return false;

    return SelectedAddonExportCommandViewModel != null;
  }

  private bool CanExecuteMoveDown(object obj)
  {
    if (SelectedAddonExportCommandViewModel == null)
      return false;

    var index = AddonExportCommandViewModels.IndexOf(SelectedAddonExportCommandViewModel);
    if (index >= AddonExportCommandViewModels.Count - 1)
      return false;

    return SelectedAddonExportCommandViewModel != null;
  }

  private bool CanExecuteExportCommandEdit(object obj)
  {
    return SelectedAddonExportCommandViewModel != null;
  }

  private bool CanExecuteExportCommandDelete(object obj)
  {
    return SelectedAddonExportCommandViewModel != null;
  }

  private bool CanExecuteExportAddon(object obj)
  {
    CanExecuteExportAddonCommand = !string.IsNullOrEmpty(Dota2AddonName) && AddonExportCommandViewModels.Any(x => x.IsChecked);
    return CanExecuteExportAddonCommand;
  }

  #endregion // Command Can Execute Handlers

  #region Command Execute Handlers

  private void ExecuteSpecifyDota2Addon(object obj)
  {
    var openFolderDialog = new OpenFolderDialog
    {
      InitialDirectory = GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsContentDirectoryInfo.FullName,
      AddToRecent = false,
      Title = "Select Dota2 addon 'content' directory"
    };

    if (openFolderDialog.ShowDialog() == true)
    {
      var selectedDirectory = new DirectoryInfo(openFolderDialog.FolderName);
      if (selectedDirectory.Exists is false)
        throw new DirectoryNotFoundException();

      // todo check if it is indeed Dota2 Addon Directory
      Dota2AddonName = selectedDirectory.Name;

      IsDirty = true;
    }
  }

  private void ExecuteSave(object obj)
  {
    if (!CanExecuteSave(obj))
      return;

    if (AddonConfigFileInfo.Exists is false)
    {
      MessageBox.Show($"File {AddonConfigFileInfo.FullName} not exists. Use \"Save as\".");
      AddonConfigFileInfo = null;
      return;
    }

    var addonExporterConfig = CreateAddonExporterConfig();

    XmlSerializerService.SerializeToXml(AddonConfigFileInfo.FullName, addonExporterConfig);

    IsDirty = false;
  }

  private void ExecuteSaveAs(object obj)
  {
    var saveFileDialog = new SaveFileDialog()
    {
      Filter = $"|*.{Constants.ADDON_EXPORT_FILE_FORMAT}|All files (*.*)|*.*"
    };

    if (saveFileDialog.ShowDialog() == true)
    {
      var addonExporterConfig = CreateAddonExporterConfig();

      XmlSerializerService.SerializeToXml(saveFileDialog.FileName, addonExporterConfig);

      AddonConfigFileInfo = new FileInfo(saveFileDialog.FileName);

      IsDirty = false;
    }
  }

  private void ExecuteMoveUp(object obj)
  {
    if (!CanExecuteMoveUp(obj))
      return;

    var index = AddonExportCommandViewModels.IndexOf(SelectedAddonExportCommandViewModel);
    AddonExportCommandViewModels.Move(index, index - 1);

    IsDirty = true;
    
    ItemStateUpdated?.Invoke(SelectedAddonExportCommandViewModel);
  }

  private void ExecuteMoveDown(object obj)
  {
    if (!CanExecuteMoveDown(obj))
      return;

    var index = AddonExportCommandViewModels.IndexOf(SelectedAddonExportCommandViewModel);
    AddonExportCommandViewModels.Move(index, index + 1);

    IsDirty = true;
    
    ItemStateUpdated?.Invoke(SelectedAddonExportCommandViewModel);
  }

  private void ExecuteExportCommandCreate(object obj)
  {
    ClickExecuteExportCommandCreate?.Invoke();
  }

  private void ExecuteExportCommandEdit(object obj)
  {
    if (!CanExecuteExportCommandEdit(obj))
      return;

    ClickExecuteExportCommandEdit?.Invoke(SelectedAddonExportCommandViewModel);
  }

  private void ExecuteExportCommandDelete(object obj)
  {
    if (!CanExecuteExportCommandDelete(obj))
      return;

    AddonExportCommandViewModels.Remove(SelectedAddonExportCommandViewModel);

    SelectedAddonExportCommandViewModel = null;

    IsDirty = true;
  }

  private void ExecuteExportCommandLoadDefaultTemplate(object obj)
  {
    if (AddonExportCommandViewModels.Count > 0)
    {
      var messageBoxResult = MessageBox.Show("All existing commands will be deleted. Continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
      if (messageBoxResult != MessageBoxResult.Yes)
        return;
    }

    AddonExportCommandViewModels.Clear();
    SelectedAddonExportCommandViewModel = null;

    AddonExportCommandViewModels.Add(new ClearOutputDirectoryViewModel()
    {
      IsChecked = true,
    });

    AddonExportCommandViewModels.Add(new CompileAddonViewModel()
    {
      IsChecked = true,
    });

    AddonExportCommandViewModels.Add(new CopyAddonDirectoryViewModel
    {
      PathToAddonDirectory = "materials",
      IsCopySubfolders = true,
      IsChecked = true,
    });

    AddonExportCommandViewModels.Add(new CopyAddonDirectoryViewModel
    {
      PathToAddonDirectory = "models",
      IsCopySubfolders = true,
      IsChecked = true,
    });

    AddonExportCommandViewModels.Add(new CopyAddonDirectoryViewModel
    {
      PathToAddonDirectory = "panorama",
      IsCopySubfolders = true,
      IsChecked = true,
    });

    AddonExportCommandViewModels.Add(new CopyAddonDirectoryViewModel
    {
      PathToAddonDirectory = "particles",
      IsCopySubfolders = true,
      IsChecked = true,
    });

    AddonExportCommandViewModels.Add(new CopyAddonDirectoryViewModel
    {
      PathToAddonDirectory = "soundevents",
      IsCopySubfolders = true,
      IsChecked = true,
    });

    AddonExportCommandViewModels.Add(new CopyAddonDirectoryViewModel
    {
      PathToAddonDirectory = "sounds",
      IsCopySubfolders = true,
      IsChecked = true,
    });

    IsDirty = true;
  }

  private void ExecuteExportAddon(object obj)
  {
    if (!CanExecuteExportAddon(obj))
      return;

    ExportAddonExecute?.Invoke(this);
  }

  #endregion // Command Execute Handlers

  #region Public Methods

  public AddonExporterInfoViewModel Clone()
  {
    var ret = new AddonExporterInfoViewModel()
    {
      IsChecked = IsChecked,
      Dota2AddonName = string.Empty,
    };

    foreach (var addonExportCommandViewModel in AddonExportCommandViewModels)
    {
      var clone = (IAddonExportCommandViewModel)addonExportCommandViewModel.Clone();
      ret.AddonExportCommandViewModels.Add(clone);
    }

    return ret;
  }

  public override void RefreshCommands()
  {
    SaveCommand.RaiseCanExecuteChanged();
    MoveUpCommand.RaiseCanExecuteChanged();
    MoveDownCommand.RaiseCanExecuteChanged();
    ExportCommandEditCommand.RaiseCanExecuteChanged();
    ExportCommandDeleteCommand.RaiseCanExecuteChanged();
    ExportAddonCommand.RaiseCanExecuteChanged();
  }

  public void HandleSuccessClickExecuteExportCommandCreate(IAddonExportCommandCreateUpdateViewModel addonExportCommandCreateUpdateViewModel)
  {
    var vm = AddonExportCommandViewModelFactory.CreateVmFromCreateUpdateVm(addonExportCommandCreateUpdateViewModel);

    vm.IsChecked = true;

    AddonExportCommandViewModels.Add(vm);
    SelectedAddonExportCommandViewModel = vm;

    IsDirty = true;
    
    ItemStateUpdated?.Invoke(vm);
  }

  public void HandleSuccessClickExecuteExportCommandEdit(IAddonExportCommandViewModel editVm, IAddonExportCommandCreateUpdateViewModel addonExportCommandCreateUpdateViewModel)
  {
    editVm.ApplyDataFromUpdateVm(addonExportCommandCreateUpdateViewModel);

    IsDirty = true;
  }

  public async Task ExportAddonAsync(IProgress<AddonExportProgress> progress)
  {
    var addonOutputDirectoryFullPath = Path.Combine(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath, Dota2AddonName);
    foreach (var addonExportCommandViewModel in AddonExportCommandViewModels)
    {
      await addonExportCommandViewModel.ExecuteExportCommandAsync(Dota2AddonName, addonOutputDirectoryFullPath, progress);
    }
  }

  #endregion // Public Methods

  #region Private Methods

  private void GlobalSettings_OnOutputDirectoryFullPathChange()
  {
    UpdateAddonOutputDirectory();
  }

  private AddonExporterDetailedConfig CreateAddonExporterConfig()
  {
    var addonExporterDetailedConfig = new AddonExporterDetailedConfig()
    {
      Dota2AddonName = Dota2AddonName,
    };

    foreach (var addonExportCommandViewModel in AddonExportCommandViewModels)
    {
      var addonExporterCommandConfig = AddonExporterCommandConfigFactory.CreateAddonExporterCommandConfig(addonExportCommandViewModel);

      addonExporterDetailedConfig.AddonExporterCommandConfigWrappers.Add(
        new AddonExporterCommandConfigWrapper()
        {
          CommandConfig = addonExporterCommandConfig
        });
    }

    return addonExporterDetailedConfig;
  }

  private void UpdateAddonOutputDirectory()
  {
    if (string.IsNullOrEmpty(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath))
    {
      AddonOutputDirectory = "*SET OUTPUT DIRECTORY FIRST*";
      return;
    }

    if (string.IsNullOrEmpty(_dota2AddonName))
    {
      AddonOutputDirectory = "*SET ADDON DIRECTORY FIRST*";
      return;
    }

    AddonOutputDirectory = Path.Combine(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath, _dota2AddonName);
  }

  #endregion // Private Methods
}