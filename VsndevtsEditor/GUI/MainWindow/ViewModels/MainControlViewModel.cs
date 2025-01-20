using System.IO;
using System.Windows;
using Common.WPF;
using CommonLib;
using Microsoft.Win32;
using SteamDatabase.ValvePak;
using ValveResourceFormat.Serialization.KeyValues;
using VsndevtsEditor.Configs;
using VsndevtsEditor.Models;

namespace VsndevtsEditor.GUI.MainWindow.ViewModels;

public class MainControlViewModel : BaseViewModel
{
  #region Fields

  private string _dota2ExecutableFullPath = string.Empty;
  private string _vsndevtsFileFullPath = string.Empty;

  private VsndevtsActionViewModel? _previousSelectedActionViewModel;
  private VsndevtsActionViewModel? _selectedActionViewModel;

  #endregion // Fields

  #region Ctor

  public MainControlViewModel()
  {
    SetPathToDota2ExeCommand = new DelegateCommand(ExecuteSetPathToDota2Exe);
    SelectVsndevtsFileCommand = new DelegateCommand(ExecuteSelectVsndevtsFile);

    Dota2ExecutableFullPath = GlobalManager.Instance.GlobalSettings.Dota2ExeFullPath;
  }

  #endregion // Ctor

  #region Properties

  public string Dota2ExecutableFullPath
  {
    get => _dota2ExecutableFullPath;
    private set
    {
      _dota2ExecutableFullPath = value;
      OnPropertyChanged();
    }
  }

  public string VsndevtsFileFullPath
  {
    get => _vsndevtsFileFullPath;
    private set
    {
      _vsndevtsFileFullPath = value;
      OnPropertyChanged();
    }
  }

  public List<VsndevtsActionViewModel> ActionViewModels { get; private set; }

  public VsndevtsActionViewModel? SelectedActionViewModel
  {
    get => _selectedActionViewModel;
    set
    {
      if (_selectedActionViewModel == value)
        return;

      _previousSelectedActionViewModel = _selectedActionViewModel;

      _selectedActionViewModel = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Commands

  public DelegateCommand SetPathToDota2ExeCommand { get; }
  public DelegateCommand SelectVsndevtsFileCommand { get; }

  #endregion // Commands

  #region Command Execute Handlers

  private void ExecuteSetPathToDota2Exe(object obj)
  {
    var resultCallDialogSetDota2ExePath = GlobalManager.Instance.CallDialogSetDota2ExePath();
    if (resultCallDialogSetDota2ExePath.IsFailure)
    {
      if (!string.IsNullOrEmpty(resultCallDialogSetDota2ExePath.Error))
      {
        MessageBox.Show(resultCallDialogSetDota2ExePath.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      return;
    }

    if (string.IsNullOrEmpty(resultCallDialogSetDota2ExePath.Value))
      return;

    GlobalManager.Instance.GlobalSettings.Dota2ExeFullPath = resultCallDialogSetDota2ExePath.Value;
    Dota2ExecutableFullPath = GlobalManager.Instance.GlobalSettings.Dota2ExeFullPath;

    var resultTrySetFullPathToDota2Exe = GlobalManager.Instance.UpdateDota2GameMainInfo();
    if (resultTrySetFullPathToDota2Exe.IsFailure)
    {
      MessageBox.Show(resultTrySetFullPathToDota2Exe.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
  }

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
          KValue = keyValuePair.Value
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

      VsndevtsFileFullPath = openFileDialog.FileName;

      ProcessVsndevtsFile(vsndevtsFile);
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
  }

  #endregion // Command Execute Handlers

  #region Private Methods

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
    ActionViewModels = new List<VsndevtsActionViewModel>();

    foreach (var action in vsndevtsFile.Actions)
    {
      ActionViewModels.Add(new VsndevtsActionViewModel(action.ActionName, action.Files));
    }

    OnPropertyChanged(nameof(ActionViewModels));
  }

  #endregion // Private Methods
}