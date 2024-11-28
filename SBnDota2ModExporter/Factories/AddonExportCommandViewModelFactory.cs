using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;
using SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy;

namespace SBnDota2ModExporter.Factories;

public static class AddonExportCommandViewModelFactory
{
  public static IAddonExportCommandViewModel CreateVmFromCreateUpdateVm(IAddonExportCommandCreateUpdateViewModel addonExportCommandCreateUpdateViewModel)
  {
    switch (addonExportCommandCreateUpdateViewModel)
    {
      case CopyAddonDirectoryCreateUpdateViewModel copyDirectoryCreateUpdateViewModel:
        return new CopyAddonDirectoryViewModel()
        {
          IsCopySubfolders = copyDirectoryCreateUpdateViewModel.IsCopySubfolders,
          DestinationOfCopyInfoViewModel = new DestinationOfCopyInfoViewModel(copyDirectoryCreateUpdateViewModel.DestinationOfCopyCreateUpdateViewModel.DestinationOfCopyDataViewModel.CreateDestinationOfCopyDataConfig()),
        };
      
      case CopyAddonFileCreateUpdateViewModel copyFileCreateUpdateViewModel:
        return new CopyAddonFileViewModel()
        {
          PathToAddonFile = copyFileCreateUpdateViewModel.PathToFile,
        };
      
      case CopyDirectoryCreateUpdateViewModel copyDirectoryCreateUpdateViewModel:
        return new CopyDirectoryViewModel()
        {
          PathToDirectory = copyDirectoryCreateUpdateViewModel.PathToDirectory,
          IsCopySubfolders = copyDirectoryCreateUpdateViewModel.IsCopySubfolders,
        };
      
      case CopyFileCreateUpdateViewModel copyFileCreateUpdateViewModel:
        return new CopyFileViewModel()
        {
          PathToFile = copyFileCreateUpdateViewModel.PathToFile,
        };
      
      case CompileAddonCreateUpdateViewModel _:
        return new CompileAddonViewModel();
      
      case ClearOutputDirectoryCreateUpdateViewModel _:
        return new ClearOutputDirectoryViewModel();

      default:
        throw new NotImplementedException();
    }
  }
}