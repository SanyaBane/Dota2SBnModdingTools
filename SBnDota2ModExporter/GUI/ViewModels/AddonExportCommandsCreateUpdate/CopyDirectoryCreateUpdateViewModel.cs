using System.IO;
using Common.WPF;
using CommonLib;
using Microsoft.Win32;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

public class CopyDirectoryCreateUpdateViewModel : BaseViewModel, IAddonExportCommandCreateUpdateViewModel
{
  #region Fields

  private readonly string _dota2AddonName;
  private readonly AddonExportOutputInfoViewModel _addonExportOutputInfoViewModel;
  private readonly Action<bool>? _canExecuteOkCommandCallback;
  private readonly CopyDirectoryViewModel? _editVm;

  private string _pathToDirectory = string.Empty;
  private bool _isCopySubfolders = true;
  private bool _isDirty;

  #endregion // Fields

  #region Ctor

  public CopyDirectoryCreateUpdateViewModel(string dota2AddonName, AddonExportOutputInfoViewModel addonExportOutputInfoViewModel, Action<bool>? canExecuteOkCommandCallback, CopyDirectoryViewModel? editVm)
  {
    _dota2AddonName = dota2AddonName;
    _addonExportOutputInfoViewModel = addonExportOutputInfoViewModel;
    _canExecuteOkCommandCallback = canExecuteOkCommandCallback;
    _editVm = editVm;

    SetPathToDirectoryCommand = new DelegateCommand(ExecuteSetPathToDirectory);

    if (editVm != null)
    {
      _pathToDirectory = editVm.PathToDirectory;
      UpdateVmAfterPathToDirectoryChange();

      _isCopySubfolders = editVm.IsCopySubfolders;
    }
  }

  #endregion // Ctor

  #region Properties

  public bool IsCreatingVm => _editVm == null;
  public bool IsUpdatingVm => !IsCreatingVm;

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

  public string? PathToDirectoryAddonRelative { get; private set; }

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
      AddToRecent = true,
    };

    if (openFolderDialog.ShowDialog() == true)
    {
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
    var resultGetDotaAddonInfo = Dota2AddonInfo.GetDotaAddonInfo(_dota2AddonName, GlobalManager.Instance.Dota2GameMainInfo);
    if (resultGetDotaAddonInfo.Success && _pathToDirectory != null && _pathToDirectory.StartsWith(resultGetDotaAddonInfo.Value.DotaAddonGameDirectoryInfo.FullName, StringComparison.InvariantCultureIgnoreCase))
      PathToDirectoryAddonRelative = _pathToDirectory.Substring(resultGetDotaAddonInfo.Value.DotaAddonGameDirectoryInfo.FullName.Length);
    else
      PathToDirectoryAddonRelative = null;

    OnPropertyChanged(nameof(PathToDirectoryAddonRelative));

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