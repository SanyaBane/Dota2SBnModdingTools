﻿using SBnDota2ModExporter.AddonExportCommands;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

public class CompileAddonViewModel : BaseAddonExportCommandViewModel
{
  public CompileAddonViewModel()
  {
  }

  #region Properties

  public override enAddonCommandType AddonCommandType => enAddonCommandType.CompileAddon;

  #endregion // Properties

  #region Public Methods

  public override void ApplyDataFromUpdateVm(IAddonExportCommandCreateUpdateViewModel createUpdateViewModel)
  {
    // do nothing
  }

  public override async Task ExecuteExportCommandAsync(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress)
  {
    var compileAddonCommand = new CompileAddonCommand(dota2AddonName, progress);
    await compileAddonCommand.Execute();
  }

  public override IAddonExportCommandViewModel Clone()
  {
    return new CompileAddonViewModel()
    {
      IsChecked = IsChecked,
    };
  }

  #endregion // Public Methods
}