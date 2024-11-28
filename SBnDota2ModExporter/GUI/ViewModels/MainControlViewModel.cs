using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Common.WPF;
using CommonLib;
using Microsoft.Win32;
using SBnDota2ModExporter.Configs.AddonsExporter;
using SBnDota2ModExporter.Configs.Main;
using SBnDota2ModExporter.Factories;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;
using SBnDota2ModExporter.GUI.Views;

namespace SBnDota2ModExporter.GUI.ViewModels;

public class MainControlViewModel : BaseViewModel
{
  #region Fields

  private string _dota2ExecutableFullPath = string.Empty;
  private string _outputDirectoryFullPath = string.Empty;

  private AddonExporterInfoViewModel? _selectedAddonExporterFileViewModel;
  private bool _isExporting;

  #endregion // Fields

  #region Ctor

  public MainControlViewModel(List<AddonExporterShortConfig> addonExporterShortConfigs)
  {
    SetPathToDota2ExeCommand = new DelegateCommand(ExecuteSetPathToDota2Exe);
    SetPathToOutputDirectoryCommand = new DelegateCommand(ExecuteSetPathToOutputDirectory);

    CreateAddonExporterFileCommand = new DelegateCommand(ExecuteCreateAddonExporterFile);
    DuplicateAddonExporterFileCommand = new DelegateCommand(ExecuteDuplicateAddonExporterFile, CanExecuteDuplicateAddonExporterFile);
    LoadAddonExporterFileCommand = new DelegateCommand(ExecuteLoadAddonExporterFile);
    RemoveAddonExporterFileCommand = new DelegateCommand(ExecuteRemoveAddonExporterFile, CanExecuteRemoveAddonExporterFile);

    ExportSelectedAddonsCommand = new DelegateCommand(ExecuteExportSelectedAddons, CanExecuteExportSelectedAddons);
    MoveUpCommand = new DelegateCommand(ExecuteMoveUp, CanExecuteMoveUp);
    MoveDownCommand = new DelegateCommand(ExecuteMoveDown, CanExecuteMoveDown);

    AddonExporterInfoViewModels = new ObservableCollection<AddonExporterInfoViewModel>();

    OutputDirectoryFullPath = GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath;
    Dota2ExecutableFullPath = GlobalManager.Instance.GlobalSettings.Dota2ExeFullPath;

    var listNotLoadedFiles = new List<string>();
    foreach (var addonExporterFullPath in addonExporterShortConfigs)
    {
      var resultCreateAddonExporterInfoViewModel = LoadAddonExporterInfoViewModelFromConfig(addonExporterFullPath);
      if (resultCreateAddonExporterInfoViewModel.Failure)
      {
        listNotLoadedFiles.Add(addonExporterFullPath.FileFullPath);
        continue;
      }

      var vm = resultCreateAddonExporterInfoViewModel.Value;

      AddonExporterInfoViewModels.Add(vm);
    }

    if (listNotLoadedFiles.Count > 0)
    {
      string text = $"Was not able to load following files:{Environment.NewLine}" +
                    $"{string.Join(Environment.NewLine, listNotLoadedFiles)}";

      MessageBox.Show(text, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    // var modExporter = new ModExporter(Dota2GameMainInfo, singleModExporterConfig);
    // var resultExportMod = modExporter.ExportMod();

    //"F:\Games\SteamLibrary\steamapps\common\dota 2 beta\game\bin\win64\resourcecompiler.exe" -r "F:\Games\SteamLibrary\steamapps\common\dota 2 beta\content\dota_addons\mod_pr_cloud_juggernaut\*.*"
  }

  #endregion // Ctor

  #region Events

  public event Action<AddonExporterInfoViewModel>? ItemStateUpdated;

  #endregion // Events

  #region Properties

  public string Dota2ExecutableFullPath
  {
    get => _dota2ExecutableFullPath;
    set
    {
      _dota2ExecutableFullPath = value;
      OnPropertyChanged();
    }
  }

  public string OutputDirectoryFullPath
  {
    get => _outputDirectoryFullPath;
    private set
    {
      _outputDirectoryFullPath = value;
      OnPropertyChanged();
    }
  }

  public ObservableCollection<AddonExporterInfoViewModel> AddonExporterInfoViewModels { get; }

  public AddonExporterInfoViewModel? SelectedAddonExporterFileViewModel
  {
    get => _selectedAddonExporterFileViewModel;
    set
    {
      _selectedAddonExporterFileViewModel = value;
      OnPropertyChanged();

      RefreshCommands();
    }
  }

  public bool IsExporting
  {
    get => _isExporting;
    set
    {
      _isExporting = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Commands

  public DelegateCommand SetPathToDota2ExeCommand { get; }
  public DelegateCommand SetPathToOutputDirectoryCommand { get; }

  public DelegateCommand CreateAddonExporterFileCommand { get; }
  public DelegateCommand DuplicateAddonExporterFileCommand { get; }
  public DelegateCommand LoadAddonExporterFileCommand { get; }
  public DelegateCommand RemoveAddonExporterFileCommand { get; }

  public DelegateCommand ExportSelectedAddonsCommand { get; }

  public DelegateCommand MoveUpCommand { get; }
  public DelegateCommand MoveDownCommand { get; }

  #endregion // Commands

  #region Command Can Execute Handlers

  private bool CanExecuteDuplicateAddonExporterFile(object obj)
  {
    return SelectedAddonExporterFileViewModel != null;
  }

  private bool CanExecuteRemoveAddonExporterFile(object obj)
  {
    return SelectedAddonExporterFileViewModel != null;
  }

  private bool CanExecuteExportSelectedAddons(object obj)
  {
    return AddonExporterInfoViewModels.Any(x => x.IsChecked && x.IsAddonValidForExport);
  }

  private bool CanExecuteMoveUp(object obj)
  {
    if (SelectedAddonExporterFileViewModel == null)
      return false;

    var index = AddonExporterInfoViewModels.IndexOf(SelectedAddonExporterFileViewModel);
    if (index <= 0)
      return false;

    return SelectedAddonExporterFileViewModel != null;
  }

  private bool CanExecuteMoveDown(object obj)
  {
    if (SelectedAddonExporterFileViewModel == null)
      return false;

    var index = AddonExporterInfoViewModels.IndexOf(SelectedAddonExporterFileViewModel);
    if (index >= AddonExporterInfoViewModels.Count - 1)
      return false;

    return SelectedAddonExporterFileViewModel != null;
  }

  #endregion // Command Can Execute Handlers

  #region Command Execute Handlers

  private void ExecuteSetPathToDota2Exe(object obj)
  {
    var resultCallDialogSetDota2ExePath = GlobalManager.Instance.CallDialogSetDota2ExePath();

    if (resultCallDialogSetDota2ExePath.Failure)
    {
      if (!string.IsNullOrEmpty(resultCallDialogSetDota2ExePath.ErrorMessage))
      {
        MessageBox.Show(resultCallDialogSetDota2ExePath.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      return;
    }

    GlobalManager.Instance.GlobalSettings.Dota2ExeFullPath = resultCallDialogSetDota2ExePath.Value;
    Dota2ExecutableFullPath = GlobalManager.Instance.GlobalSettings.Dota2ExeFullPath;

    var resultTrySetFullPathToDota2Exe = GlobalManager.Instance.UpdateDota2GameMainInfo();
    if (resultTrySetFullPathToDota2Exe.Failure)
    {
      MessageBox.Show(resultTrySetFullPathToDota2Exe.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
  }

  private void ExecuteSetPathToOutputDirectory(object obj)
  {
    var openFolderDialog = new OpenFolderDialog
    {
      AddToRecent = false,
      Title = "Select output directory"
    };

    if (openFolderDialog.ShowDialog() == true)
    {
      if (Directory.Exists(openFolderDialog.FolderName))
      {
        OutputDirectoryFullPath = openFolderDialog.FolderName;
        GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath = OutputDirectoryFullPath;
      }
      else
      {
        MessageBox.Show($"Directory does not exist:{Environment.NewLine}" +
                        $"'{openFolderDialog.FolderName}'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }

  private Result CanSaveAsFile(AddonExporterInfoViewModel addonExporterInfoViewModel, string fullPathToFile)
  {
    var vmsToCheck = AddonExporterInfoViewModels
      .Where(x => !ReferenceEquals(x, addonExporterInfoViewModel) && x.AddonConfigFileInfo != null);

    if (vmsToCheck.Any(x => string.Equals(x.AddonConfigFileInfo.FullName, fullPathToFile, StringComparison.InvariantCultureIgnoreCase)))
      return new Result("Selected file is already loaded.");

    return new Result(true);
  }

  private void ExecuteCreateAddonExporterFile(object obj)
  {
    var createdVm = new AddonExporterInfoViewModel(CanSaveAsFile)
    {
      IsChecked = true
    };

    AddonExporterInfoViewModels.Add(createdVm);

    SelectedAddonExporterFileViewModel = createdVm;

    SubscribeToNewAddonExporterInfoViewModel(createdVm);

    ItemStateUpdated?.Invoke(SelectedAddonExporterFileViewModel);
  }

  private void ExecuteDuplicateAddonExporterFile(object obj)
  {
    if (SelectedAddonExporterFileViewModel == null)
      throw new NullReferenceException();

    var selectedItem = SelectedAddonExporterFileViewModel;

    var clonedVm = selectedItem.Clone();

    AddonExporterInfoViewModels.Add(clonedVm);

    SelectedAddonExporterFileViewModel = clonedVm;

    SubscribeToNewAddonExporterInfoViewModel(clonedVm);
  }

  private void ExecuteLoadAddonExporterFile(object obj)
  {
    var openFileDialog = new OpenFileDialog()
    {
      Filter = $"(*.{Constants.ADDON_EXPORT_FILE_FORMAT})|*.{Constants.ADDON_EXPORT_FILE_FORMAT}|All files (*.*)|*.*",
      Multiselect = true
    };

    if (openFileDialog.ShowDialog() != true)
      return;

    var alreadyLoadedFiles = new List<string>();
    var notLoadedFiles = new List<Tuple<string, string>>();
    var loadedAddonVms = new List<AddonExporterInfoViewModel>();
    foreach (var fileName in openFileDialog.FileNames)
    {
      if (AddonExporterInfoViewModels.Any(x => x.AddonConfigFileInfo.FullName == fileName))
      {
        alreadyLoadedFiles.Add(fileName);
        continue;
      }

      var addonExporterShortConfig = new AddonExporterShortConfig()
      {
        FileFullPath = fileName
      };

      var resultCreateAddonExporterInfoViewModel = LoadAddonExporterInfoViewModelFromConfig(addonExporterShortConfig);
      if (resultCreateAddonExporterInfoViewModel.Failure)
      {
        notLoadedFiles.Add(new Tuple<string, string>(fileName, resultCreateAddonExporterInfoViewModel.ErrorMessage));
        continue;
      }

      resultCreateAddonExporterInfoViewModel.Value.IsChecked = true;

      var vm = resultCreateAddonExporterInfoViewModel.Value;

      loadedAddonVms.Add(vm);
    }

    if (loadedAddonVms.Count > 0)
    {
      foreach (var addonExporterInfoViewModel in loadedAddonVms)
      {
        AddonExporterInfoViewModels.Add(addonExporterInfoViewModel);

        SelectedAddonExporterFileViewModel = loadedAddonVms.Last();
      }
    }

    if (alreadyLoadedFiles.Count > 0)
    {
      string text = $"Following files are skipped because they are already loaded:{Environment.NewLine}" +
                    $"{string.Join(Environment.NewLine, alreadyLoadedFiles)}";

      MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
    }

    if (notLoadedFiles.Count > 0)
    {
      string text = $"Some files were not loaded:{Environment.NewLine}" +
                    $"{string.Join(Environment.NewLine, notLoadedFiles.Select(x => x.Item1))}";

      MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
    }
  }

  private void ExecuteRemoveAddonExporterFile(object obj)
  {
    if (SelectedAddonExporterFileViewModel == null)
      throw new NullReferenceException();

    var selectedItem = SelectedAddonExporterFileViewModel;
    AddonExporterInfoViewModels.Remove(selectedItem);
  }

  private void ExecuteExportSelectedAddons(object obj)
  {
    if (string.IsNullOrEmpty(OutputDirectoryFullPath))
    {
      MessageBox.Show("You didn't set output directory", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
      return;
    }

    if (AddonExporterInfoViewModels.Count == 0)
    {
      MessageBox.Show("You have no addon export files loaded", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
      return;
    }

    var addonsToExport = AddonExporterInfoViewModels.Where(x => x.IsChecked).ToArray();
    if (addonsToExport.Length == 0)
    {
      MessageBox.Show("You have no addon export files selected for export", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
      return;
    }

    var exportProgressWindow = new ExportProgressWindow()
    {
      AddonsToExport = addonsToExport,
    };

    IsExporting = true;

    try
    {
      exportProgressWindow.ShowDialog();
    }
    finally
    {
      IsExporting = false;
    }
  }

  private void ExecuteMoveUp(object obj)
  {
    if (!CanExecuteMoveUp(obj))
      return;

    var index = AddonExporterInfoViewModels.IndexOf(SelectedAddonExporterFileViewModel);
    AddonExporterInfoViewModels.Move(index, index - 1);

    RefreshCommands();

    ItemStateUpdated?.Invoke(SelectedAddonExporterFileViewModel);
  }

  private void ExecuteMoveDown(object obj)
  {
    if (!CanExecuteMoveDown(obj))
      return;

    var index = AddonExporterInfoViewModels.IndexOf(SelectedAddonExporterFileViewModel);
    AddonExporterInfoViewModels.Move(index, index + 1);

    RefreshCommands();

    ItemStateUpdated?.Invoke(SelectedAddonExporterFileViewModel);
  }

  #endregion // Command Execute Handlers

  #region Public Methods

  public override void RefreshCommands()
  {
    RemoveAddonExporterFileCommand.RaiseCanExecuteChanged();
    DuplicateAddonExporterFileCommand.RaiseCanExecuteChanged();
    ExportSelectedAddonsCommand.RaiseCanExecuteChanged();
    MoveUpCommand.RaiseCanExecuteChanged();
    MoveDownCommand.RaiseCanExecuteChanged();
  }

  #endregion // Public Methods

  #region Private Methods

  private void SubscribeToNewAddonExporterInfoViewModel(AddonExporterInfoViewModel addonExporterInfoViewModel)
  {
    addonExporterInfoViewModel.IsCheckedChange += AddonExporterInfoViewModel_OnIsCheckedChange;
    addonExporterInfoViewModel.IsAddonValidForExportChange += AddonExporterInfoViewModel_OnIsAddonValidForExportChange;
    addonExporterInfoViewModel.ExportAddonExecute += AddonExporterInfoViewModel_ExportAddonExecute;
  }

  private void AddonExporterInfoViewModel_OnIsCheckedChange()
  {
    RefreshCommands();
  }

  private void AddonExporterInfoViewModel_OnIsAddonValidForExportChange()
  {
    RefreshCommands();
  }

  private void AddonExporterInfoViewModel_ExportAddonExecute(AddonExporterInfoViewModel addonExporterInfoViewModel)
  {
    var exportProgressWindow = new ExportProgressWindow()
    {
      AddonsToExport = [addonExporterInfoViewModel],
    };

    IsExporting = true;

    try
    {
      exportProgressWindow.ShowDialog();
    }
    finally
    {
      IsExporting = false;
    }
  }

  private Result<AddonExporterInfoViewModel?> LoadAddonExporterInfoViewModelFromConfig(AddonExporterShortConfig addonExporterShortConfig)
  {
    try
    {
      var addonExporterDetailedConfig = XmlSerializerService.DeserilazeFromXml<AddonExporterDetailedConfig>(addonExporterShortConfig.FileFullPath);

      var loadedVm = new AddonExporterInfoViewModel(CanSaveAsFile)
      {
        AddonConfigFileInfo = new FileInfo(addonExporterShortConfig.FileFullPath),
        IsChecked = addonExporterShortConfig.IsChecked,
        Dota2AddonName = addonExporterDetailedConfig.Dota2AddonName,
        AddonExportOutputInfoViewModel =
        {
          CustomOutputDirectoryName = addonExporterDetailedConfig.AddonOutputDirectoryName,
        },
        IsDirty = false,
      };

      var tempList = new List<IAddonExportCommandViewModel>();
      foreach (var addonExporterCommandConfigWrapper in addonExporterDetailedConfig.AddonExporterCommandConfigWrappers)
      {
        var addonExportCommandViewModel = AddonExporterCommandConfigFactory.CreateAddonExportCommandViewModel(addonExporterCommandConfigWrapper.CommandConfig, addonExporterDetailedConfig.Dota2AddonName, loadedVm.AddonExportOutputInfoViewModel);
        tempList.Add(addonExportCommandViewModel);
      }

      foreach (var addonExportCommandViewModel in tempList.OrderBy(x => x.Index))
      {
        loadedVm.AddonExportCommandViewModels.Add(addonExportCommandViewModel);
      }

      SubscribeToNewAddonExporterInfoViewModel(loadedVm);

      return new Result<AddonExporterInfoViewModel?>(true, loadedVm);
    }
    catch (Exception ex)
    {
      return new Result<AddonExporterInfoViewModel?>(ex.Message, ex);
    }
  }

  #endregion // Private Methods
}