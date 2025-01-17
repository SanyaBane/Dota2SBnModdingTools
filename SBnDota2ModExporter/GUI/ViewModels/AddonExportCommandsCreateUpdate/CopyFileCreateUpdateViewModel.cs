﻿using System.IO;
using Common.WPF;
using CommonLib;
using Microsoft.Win32;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

public class CopyFileCreateUpdateViewModel : BaseViewModel, IAddonExportCommandCreateUpdateViewModel
{
  #region Fields

  private readonly string _dota2AddonName;
  private readonly AddonExportOutputInfoViewModel _addonExportOutputInfoViewModel;
  private readonly Action<bool>? _canExecuteOkCommandCallback;
  private readonly CopyFileViewModel? _editVm;

  private string _pathToFile = string.Empty;
  private bool _isDirty;

  #endregion // Fields

  #region Ctor

  public CopyFileCreateUpdateViewModel(string dota2AddonName, AddonExportOutputInfoViewModel addonExportOutputInfoViewModel, Action<bool>? canExecuteOkCommandCallback, CopyFileViewModel? editVm)
  {
    _dota2AddonName = dota2AddonName;
    _addonExportOutputInfoViewModel = addonExportOutputInfoViewModel;
    _canExecuteOkCommandCallback = canExecuteOkCommandCallback;
    _editVm = editVm;

    SetPathToFileCommand = new DelegateCommand(ExecuteSetPathToFile);

    if (editVm != null)
    {
      _pathToFile = editVm.PathToFile;
      UpdateVmAfterPathToFileChange();
    }
  }

  #endregion // Ctor

  #region Properties

  public bool IsCreatingVm => _editVm == null;
  public bool IsUpdatingVm => !IsCreatingVm;

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

  public string? PathToFileAddonRelative { get; private set; }

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
    var resultGetDotaAddonInfo = Dota2AddonInfo.GetDotaAddonInfo(_dota2AddonName, GlobalManager.Instance.Dota2GameMainInfo);
    if (resultGetDotaAddonInfo.IsSuccess && _pathToFile != null && _pathToFile.StartsWith(resultGetDotaAddonInfo.Value.DotaAddonGameDirectoryInfo.FullName, StringComparison.InvariantCultureIgnoreCase))
      PathToFileAddonRelative = _pathToFile.Substring(resultGetDotaAddonInfo.Value.DotaAddonGameDirectoryInfo.FullName.Length);
    else
      PathToFileAddonRelative = null;

    OnPropertyChanged(nameof(PathToFileAddonRelative));

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