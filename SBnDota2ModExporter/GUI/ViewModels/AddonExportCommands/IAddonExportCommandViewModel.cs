using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

public interface IAddonExportCommandViewModel
{
  event Action? IsCheckedChange;

  enAddonCommandType AddonCommandType { get; }
  string Name { get; }
  bool IsChecked { get; set; }
  int Index { get; set; }

  void ApplyDataFromUpdateVm(IAddonExportCommandCreateUpdateViewModel createUpdateViewModel);
  Task ExecuteExportCommandAsync(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress);

  IAddonExportCommandViewModel Clone();
}