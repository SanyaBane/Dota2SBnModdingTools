using System.IO;
using Common.WPF;
using Microsoft.Win32;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

public class CopyAddonFileCreateUpdateViewModel : BaseViewModel, IAddonExportCommandCreateUpdateViewModel
{
  #region Fields

  private readonly string _dota2AddonName;
  private readonly Action<bool>? _canExecuteOkCommandCallback;

  private string _pathToFile = string.Empty;
  private bool _isDirty;

  #endregion // Fields

  #region Ctor

  public CopyAddonFileCreateUpdateViewModel(string dota2AddonName, Action<bool>? canExecuteOkCommandCallback, CopyAddonFileViewModel? editVm)
  {
    _dota2AddonName = dota2AddonName;
    _canExecuteOkCommandCallback = canExecuteOkCommandCallback;

    SetPathToFileCommand = new DelegateCommand(ExecuteSetPathToFile);

    if (editVm != null)
    {
      _pathToFile = editVm.PathToAddonFile;
      UpdateVmAfterPathToFileChange();
    }
  }

  #endregion // Ctor

  #region Properties

  public string PathToFile
  {
    get => _pathToFile;
    set
    {
      _pathToFile = value;
      OnPropertyChanged();

      UpdateVmAfterPathToFileChange();

      IsDirty = true;
    }
  }

  public string? PreviewResult { get; private set; }

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

  public DelegateCommand SetPathToFileCommand { get; }

  #endregion // Commands

  #region Command Execute Handlers

  private void ExecuteSetPathToFile(object obj)
  {
    throw new NotImplementedException();
    
    var addonDirectoryFullPath = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, _dota2AddonName);
    var addonDirectoryInfo = new DirectoryInfo(addonDirectoryFullPath);

    FileInfo? fileInfo = null;
    if (!string.IsNullOrEmpty(PathToFile))
    {
      fileInfo = new FileInfo(PathToFile);
    }

    string? initialDirectory = null;
    if (fileInfo is { Exists: true, Directory.Exists: true })
    {
      initialDirectory = fileInfo.Directory.FullName;
    }

    var openFileDialog = new OpenFileDialog()
    {
      InitialDirectory = initialDirectory ?? addonDirectoryInfo.FullName
    };

    if (openFileDialog.ShowDialog() == true)
    {
      PathToFile = openFileDialog.FileName;
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
    return IsDirty && !string.IsNullOrEmpty(PathToFile);
  }

  private void UpdateVmAfterPathToFileChange()
  {
    if (string.IsNullOrEmpty(_pathToFile))
    {
      PreviewResult = string.Empty;
    }
    else
    {
      var file = new FileInfo(_pathToFile);
      PreviewResult = Path.Combine(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath, _dota2AddonName, file.Name);
    }

    OnPropertyChanged(nameof(PreviewResult));
  }

  #endregion // Private Methods
}