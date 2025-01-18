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

  public override async Task ExecuteExportCommandAsync(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress)
  {
    var clearOutputDirectoryCommand = new ClearOutputDirectoryCommand(addonOutputDirectoryFullPath, progress);
    await clearOutputDirectoryCommand.Execute();
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