using SBnDota2ModExporter.AddonExportCommands;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

public class ClearOutputDirectoryViewModel : BaseAddonExportCommandViewModel
{
  public ClearOutputDirectoryViewModel()
  {
  }

  #region Properties

  public override enAddonCommandType AddonCommandType => enAddonCommandType.ClearOutputDirectory;

  #endregion // Properties

  #region Public Methods

  public override void ApplyDataFromUpdateVm(IAddonExportCommandCreateUpdateViewModel createUpdateViewModel)
  {
    // do nothing
  }

  public override Task ExecuteExportCommandAsync(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress)
  {
    ClearOutputDirectoryCommand.Execute(dota2AddonName, addonOutputDirectoryFullPath, progress);
    return Task.CompletedTask;
  }

  public override IAddonExportCommandViewModel Clone()
  {
    return new ClearOutputDirectoryViewModel()
    {
      IsChecked = IsChecked,
    };
  }

  #endregion // Public Methods
}