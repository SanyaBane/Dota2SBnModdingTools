using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using Common.WPF;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
using ValveResourceFormat.Serialization.KeyValues;
using VsndevtsEditor.Configs;
using VsndevtsEditor.GUI.MainWindow.Messages;
using VsndevtsEditor.Models;

namespace VsndevtsEditor.GUI.MainWindow.ViewModels;

public class MainControlViewModel : BaseViewModel
{
  #region Fields

  private LoadedVsndevtsFileViewModel? _loadedVsndevtsFileViewModel;
  private ObservableCollection<VsndevtsActionViewModel> _selectedActionViewModels;

  private string _relativePathToAddonSoundsDirectory = "sounds/sanyabane/bathory_magnus";

  #endregion // Fields

  #region Ctor

  public MainControlViewModel()
  {
    SelectVsndevtsFileCommand = new DelegateCommand(ExecuteSelectVsndevtsFile);

    _selectedActionViewModels = new ObservableCollection<VsndevtsActionViewModel>();
    _selectedActionViewModels.CollectionChanged += SelectedActionViewModelsOnCollectionChanged;

    RefreshFilesInTemplateDirectoriesCommand = new DelegateCommand(ExecuteRefreshFilesInTemplateDirectories, CanExecuteRefreshFilesInTemplateDirectories);
    AutoPopulateSelectedActionsCommand = new DelegateCommand(ExecuteAutoPopulateSelectedActions, CanExecuteAutoPopulateSelectedActions);
    AutoPopulateAllActionsCommand = new DelegateCommand(ExecuteAutoPopulateAllActions, CanExecuteAutoPopulateAllActions);
    SetSelectedActionsToNullCommand = new DelegateCommand(ExecuteSetSelectedActionsToNull, CanExecuteSetSelectedActionsToNull);
    SetAllActionsToNullCommand = new DelegateCommand(ExecuteSetAllActionsToNull, CanExecuteSetAllActionsToNull);
    SaveFileAsCommand = new DelegateCommand(ExecuteSaveFileAs, CanExecuteSaveFileAs);
  }

  #endregion // Ctor

  #region Properties

  public LoadedVsndevtsFileViewModel? LoadedVsndevtsFileViewModel
  {
    get => _loadedVsndevtsFileViewModel;
    private set
    {
      _loadedVsndevtsFileViewModel = value;
      OnPropertyChanged();

      RefreshCommands();
    }
  }

  public ObservableCollection<VsndevtsActionViewModel>? ActionViewModels { get; private set; }

  public ObservableCollection<VsndevtsActionViewModel> SelectedActionViewModels
  {
    get => _selectedActionViewModels;
    set
    {
      _selectedActionViewModels = value;
      OnPropertyChanged();
    }
  }

  public string RelativePathToAddonSoundsDirectory
  {
    get => _relativePathToAddonSoundsDirectory;
    set
    {
      _relativePathToAddonSoundsDirectory = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Event Handlers

  private void SelectedActionViewModelsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    RefreshCommands();
  }

  #endregion // Event Handlers

  #region Commands

  public DelegateCommand SelectVsndevtsFileCommand { get; }
  public DelegateCommand RefreshFilesInTemplateDirectoriesCommand { get; }
  public DelegateCommand AutoPopulateSelectedActionsCommand { get; }
  public DelegateCommand SetSelectedActionsToNullCommand { get; }
  public DelegateCommand AutoPopulateAllActionsCommand { get; }
  public DelegateCommand SetAllActionsToNullCommand { get; }
  public DelegateCommand SaveFileAsCommand { get; }

  #endregion // Commands

  #region Command Execute Handlers

  private void ExecuteSelectVsndevtsFile(object obj)
  {
    var openFileDialog = new OpenFileDialog()
    {
      Filter = $"(*.{Constants.VSNDEVTS_FILE_FORMAT})|*.{Constants.VSNDEVTS_FILE_FORMAT}|All files (*.*)|*.*",
      Multiselect = false
    };

    if (openFileDialog.ShowDialog() != true)
      return;

    try
    {
      GlobalManager.Instance.TemplateDirectoriesSettings.UpdateFileInfosInTemplateDirectories();

      var parsedKv3File = KeyValues3.ParseKVFile(openFileDialog.FileName);

      var vsndevtsFile = new VsndevtsFile()
      {
        Kv3File = parsedKv3File
      };

      foreach (var keyValuePair in parsedKv3File.Root.Properties)
      {
        string soundActionName = keyValuePair.Key;

        if (keyValuePair.Value is not KVValue kvValue)
          continue;

        if (kvValue.Value is not KVObject kvObject)
          continue;

        var vsndevtsAction = new VsndevtsAction()
        {
          ActionName = soundActionName,
          KVValue = keyValuePair.Value
        };

        foreach (var kvObjectProperty in kvObject.Properties)
        {
          if (kvObjectProperty.Key != "vsnd_files")
            continue;

          if (kvObjectProperty.Value is not KVValue vsndFilesKvValue)
            continue;

          if (vsndFilesKvValue.Type == KVType.STRING)
          {
            var vsndevtsFileRelativePath = vsndFilesKvValue.Value.ToString();

            vsndevtsAction.AddVsndActionFile(new VsndevtsActionFile()
            {
              PathToFile = vsndevtsFileRelativePath,
              KVValue = vsndFilesKvValue,
              KVObjectContainer = kvObject
            });
          }
          else if (vsndFilesKvValue.Value is KVObject kvObject2)
          {
            ParseVsndFilesArrayNode(kvObject2, vsndevtsAction);
          }
        }

        vsndevtsFile.AddVsndevtsAction(vsndevtsAction);
      }

      LoadedVsndevtsFileViewModel = new LoadedVsndevtsFileViewModel(openFileDialog.FileName, parsedKv3File, vsndevtsFile);

      ProcessVsndevtsFile(LoadedVsndevtsFileViewModel.VsndevtsFile);
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
  }

  private void ExecuteRefreshFilesInTemplateDirectories(object obj)
  {
    GlobalManager.Instance.TemplateDirectoriesSettings.UpdateFileInfosInTemplateDirectories();

    foreach (var actionVm in ActionViewModels)
    {
      actionVm.UpdateTemplateDirectoryData();
    }
  }

  private void ExecuteAutoPopulateSelectedActions(object obj)
  {
    AutoPopulateSelectedActions(SelectedActionViewModels.ToArray());
  }

  private void ExecuteAutoPopulateAllActions(object obj)
  {
    AutoPopulateSelectedActions(ActionViewModels);
  }

  private void ExecuteSetSelectedActionsToNull(object obj)
  {
    SetActionsToNull(SelectedActionViewModels.ToArray());
  }

  private void ExecuteSetAllActionsToNull(object obj)
  {
    WeakReferenceMessenger.Default.Send(new CallAreYouSureWindowMessage());
  }

  private void ExecuteSaveFileAs(object obj)
  {
    var vsndevtsFileName = Path.GetFileName(LoadedVsndevtsFileViewModel.FileFullPath);
    var saveFileDialog = new SaveFileDialog()
    {
      FileName = vsndevtsFileName,
      Filter = $"|*.{Constants.VSNDEVTS_FILE_FORMAT}|All files (*.*)|*.*"
    };

    if (saveFileDialog.ShowDialog() == true)
    {
      var dirtyViewModels = ActionViewModels.Where(x => x.IsDirty);
      foreach (var vsndevtsActionViewModel in dirtyViewModels)
      {
        ApplyChangesToKVValues(vsndevtsActionViewModel);
      }

      using var indentedTextWriter = new ValveResourceFormat.IndentedTextWriter();
      LoadedVsndevtsFileViewModel.ParsedKv3File.WriteText(indentedTextWriter);

      File.WriteAllText(saveFileDialog.FileName, indentedTextWriter.ToString());
    }
  }

  #endregion // Command Execute Handlers

  #region Can Execute Handlers

  private bool CanExecuteRefreshFilesInTemplateDirectories(object obj)
  {
    if (LoadedVsndevtsFileViewModel == null)
      return false;

    return true;
  }

  private bool CanExecuteAutoPopulateSelectedActions(object obj)
  {
    if (LoadedVsndevtsFileViewModel == null || SelectedActionViewModels.Count == 0)
      return false;

    if (SelectedActionViewModels.All(x => x.TemplateDirectoryData == null || x.TemplateDirectoryData.FoundFiles.Length == 0))
      return false;

    return true;
  }

  private bool CanExecuteAutoPopulateAllActions(object obj)
  {
    if (LoadedVsndevtsFileViewModel == null)
      return false;

    return true;
  }

  private bool CanExecuteSetSelectedActionsToNull(object obj)
  {
    if (LoadedVsndevtsFileViewModel == null || SelectedActionViewModels.Count == 0)
      return false;

    return true;
  }

  private bool CanExecuteSetAllActionsToNull(object obj)
  {
    if (LoadedVsndevtsFileViewModel == null)
      return false;

    return true;
  }

  private bool CanExecuteSaveFileAs(object obj)
  {
    return LoadedVsndevtsFileViewModel != null;
  }

  #endregion // Can Execute Handlers

  #region Public Methods

  public override void RefreshCommands()
  {
    base.RefreshCommands();

    RefreshFilesInTemplateDirectoriesCommand.RaiseCanExecuteChanged();
    AutoPopulateSelectedActionsCommand.RaiseCanExecuteChanged();
    AutoPopulateAllActionsCommand.RaiseCanExecuteChanged();
    SetSelectedActionsToNullCommand.RaiseCanExecuteChanged();
    SetAllActionsToNullCommand.RaiseCanExecuteChanged();
    SaveFileAsCommand.RaiseCanExecuteChanged();
  }

  public void SetActionsToNull(IEnumerable<VsndevtsActionViewModel> actionViewModels)
  {
    foreach (var vsndevtsActionViewModel in actionViewModels)
    {
      vsndevtsActionViewModel.ClearActionFileVms();

      var relativePathWithSoundFile = Path.Combine(RelativePathToAddonSoundsDirectory, "null.mp3");
      relativePathWithSoundFile = relativePathWithSoundFile.Replace('\\', '/');

      var newVsndevtsActionFileViewModel = new VsndevtsActionFileViewModel(relativePathWithSoundFile, true);
      vsndevtsActionViewModel.AddActionFileVm(newVsndevtsActionFileViewModel);
      vsndevtsActionViewModel.IsDirty = true;
    }
  }

  #endregion // Public Methods

  #region Private Methods

  private void AutoPopulateSelectedActions(IEnumerable<VsndevtsActionViewModel> actionViewModels)
  {
    var filteredActionViewModels = actionViewModels.Where(x => x.TemplateDirectoryData != null && x.TemplateDirectoryData.FoundFiles.Length > 0);
    var lookup = filteredActionViewModels.ToLookup(x => x.TemplateDirectoryData.DirectoryName);
    foreach (var grouping in lookup)
    {
      int i = 0;
      foreach (var vsndevtsActionViewModel in grouping)
      {
        var pathToUserSoundFile = vsndevtsActionViewModel.TemplateDirectoryData.FoundFiles[i].Name;

        vsndevtsActionViewModel.ClearActionFileVms();

        var relativePathWithSoundFile = Path.Combine(RelativePathToAddonSoundsDirectory, vsndevtsActionViewModel.TemplateDirectoryData.DirectoryName, pathToUserSoundFile);
        relativePathWithSoundFile = relativePathWithSoundFile.Replace('\\', '/');

        var newVsndevtsActionFileViewModel = new VsndevtsActionFileViewModel(relativePathWithSoundFile, true);
        vsndevtsActionViewModel.AddActionFileVm(newVsndevtsActionFileViewModel);
        vsndevtsActionViewModel.IsDirty = true;

        i++;

        if (i >= vsndevtsActionViewModel.TemplateDirectoryData.FoundFiles.Length)
          i = 0;
      }
    }
  }

  private void ApplyChangesToKVValues(VsndevtsActionViewModel vsndevtsActionViewModel)
  {
    var actionKVValue = vsndevtsActionViewModel.VsndevtsAction.KVValue;
    var container = actionKVValue.Value as KVObject;
    if (container == null)
    {
      throw new Exception(nameof(ApplyChangesToKVValues));
    }

    container.Properties.Remove("vsnd_files");

    if (vsndevtsActionViewModel.ActionFileVms.Count == 0)
      return;

    KVValue newVsndFilesKValue;
    if (vsndevtsActionViewModel.ActionFileVms.Count == 1)
    {
      newVsndFilesKValue = new KVValue(KVType.STRING, vsndevtsActionViewModel.ActionFileVms.Single().PathToFile);
    }
    else
    {
      var kvObjectArray = new KVObject("vsnd_files", true, vsndevtsActionViewModel.ActionFileVms.Count);
      for (var index = 0; index < vsndevtsActionViewModel.ActionFileVms.Count; index++)
      {
        var vsndevtsActionFileViewModel = vsndevtsActionViewModel.ActionFileVms[index];
        kvObjectArray.AddProperty(index.ToString(), new KVValue(KVType.STRING, vsndevtsActionFileViewModel.PathToFile));
      }

      newVsndFilesKValue = new KVValue(KVType.ARRAY, kvObjectArray);
    }

    container.AddProperty("vsnd_files", newVsndFilesKValue);
  }

  private static void ParseVsndFilesArrayNode(KVObject kvObject2, VsndevtsAction vsndevtsAction)
  {
    foreach (var vsndFileProperty in kvObject2.Properties)
    {
      if (vsndFileProperty.Value is not KVValue singleVsndFileKvValue)
        continue;

      var vsndevtsFileRelativePath = singleVsndFileKvValue.Value.ToString();
      vsndevtsAction.AddVsndActionFile(new VsndevtsActionFile()
      {
        PathToFile = vsndevtsFileRelativePath,
        KVValue = singleVsndFileKvValue,
        KVObjectContainer = kvObject2,
      });
    }
  }

  private void ProcessVsndevtsFile(VsndevtsFile vsndevtsFile)
  {
    ActionViewModels = [];

    foreach (var action in vsndevtsFile.Actions)
    {
      var actionVm = new VsndevtsActionViewModel(action);
      actionVm.UpdateTemplateDirectoryData();
      ActionViewModels.Add(actionVm);
    }

    OnPropertyChanged(nameof(ActionViewModels));
  }

  #endregion // Private Methods
}