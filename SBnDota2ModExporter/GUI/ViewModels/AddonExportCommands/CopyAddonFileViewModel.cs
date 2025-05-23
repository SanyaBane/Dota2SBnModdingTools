﻿using SBnDota2ModExporter.AddonExportCommands;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

public class CopyAddonFileViewModel : BaseAddonExportCommandViewModel
{
  #region Fields

  private string _pathToAddonFile = string.Empty;

  #endregion // Fields

  public CopyAddonFileViewModel()
  {
  }

  #region Properties

  public override enAddonCommandType AddonCommandType => enAddonCommandType.CopyAddonFile;

  public string PathToAddonFile
  {
    get => _pathToAddonFile;
    set
    {
      _pathToAddonFile = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Public Methods

  public override void ApplyDataFromUpdateVm(IAddonExportCommandCreateUpdateViewModel createUpdateViewModel)
  {
    var createUpdateVm = (CopyAddonFileCreateUpdateViewModel)createUpdateViewModel;
    PathToAddonFile = createUpdateVm.PathToFile;
  }

  public override async Task ExecuteExportCommandAsync(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress)
  {
    var copyAddonFileCommand = new CopyAddonFileCommand(dota2AddonName, addonOutputDirectoryFullPath, progress, PathToAddonFile);
    await copyAddonFileCommand.Execute();
  }

  public override IAddonExportCommandViewModel Clone()
  {
    return new CopyAddonFileViewModel()
    {
      PathToAddonFile = PathToAddonFile,
      IsChecked = IsChecked,
    };
  }

  #endregion // Public Methods
}