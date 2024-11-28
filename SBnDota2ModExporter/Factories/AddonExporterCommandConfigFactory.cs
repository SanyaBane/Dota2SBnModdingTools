using SBnDota2ModExporter.Configs.AddonsExporter;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;
using SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy;

namespace SBnDota2ModExporter.Factories;

public static class AddonExporterCommandConfigFactory
{
  public static BaseAddonExporterCommandConfig CreateAddonExporterCommandConfig(IAddonExportCommandViewModel addonExportCommandViewModel)
  {
    switch (addonExportCommandViewModel)
    {
      case CopyAddonDirectoryViewModel copyDirectoryViewModel:
        return new CopyAddonDirectoryCommandConfig()
        {
          IsChecked = copyDirectoryViewModel.IsChecked,
          IsCopySubfolders = copyDirectoryViewModel.IsCopySubfolders,
          DestinationOfCopyConfigWrapper = copyDirectoryViewModel.DestinationOfCopyInfoViewModel.CreateDestinationOfCopyConfig(),
        };

      case CopyAddonFileViewModel copyFileViewModel:
        return new CopyAddonFileCommandConfig()
        {
          IsChecked = copyFileViewModel.IsChecked,
          PathToFile = copyFileViewModel.PathToAddonFile,
        };
      
      case CopyDirectoryViewModel copyDirectoryViewModel:
        return new CopyDirectoryCommandConfig()
        {
          IsChecked = copyDirectoryViewModel.IsChecked,
          PathToDirectory = copyDirectoryViewModel.PathToDirectory,
          IsCopySubfolders = copyDirectoryViewModel.IsCopySubfolders,
        };

      case CopyFileViewModel copyFileViewModel:
        return new CopyFileCommandConfig()
        {
          IsChecked = copyFileViewModel.IsChecked,
          PathToFile = copyFileViewModel.PathToFile,
        };
      
      case CompileAddonViewModel compileAddonViewModel:
        return new CompileAddonCommandConfig()
        {
          IsChecked = compileAddonViewModel.IsChecked,
        };
      
      case ClearOutputDirectoryViewModel clearOutputDirectoryViewModel:
        return new ClearOutputDirectoryCommandConfig()
        {
          IsChecked = clearOutputDirectoryViewModel.IsChecked,
        };

      default:
        throw new NotImplementedException();
    }
  }

  public static IAddonExportCommandViewModel CreateAddonExportCommandViewModel(BaseAddonExporterCommandConfig addonExporterCommandConfig, string? dota2AddonName, AddonExportOutputInfoViewModel loadedVmAddonExportOutputInfoViewModel)
  {
    switch (addonExporterCommandConfig)
    {
      case CopyAddonDirectoryCommandConfig copyDirectoryCommandConfig:
        return new CopyAddonDirectoryViewModel()
        {
          IsChecked = copyDirectoryCommandConfig.IsChecked,
          IsCopySubfolders = copyDirectoryCommandConfig.IsCopySubfolders,
          // DestinationOfCopyInfoViewModel = copyDirectoryCommandConfig.DestinationOfCopyConfigWrapper.CreateDestinationOfCopyCreateUpdateViewModel(dota2AddonName, loadedVmAddonExportOutputInfoViewModel),
          DestinationOfCopyInfoViewModel = new DestinationOfCopyInfoViewModel(copyDirectoryCommandConfig.DestinationOfCopyConfigWrapper.DestinationOfCopyDataConfig),
        };

      case CopyAddonFileCommandConfig copyFileCommandConfig:
        return new CopyAddonFileViewModel()
        {
          IsChecked = copyFileCommandConfig.IsChecked,
          PathToAddonFile = copyFileCommandConfig.PathToFile,
        };
      
      case CopyDirectoryCommandConfig copyDirectoryCommandConfig:
        return new CopyDirectoryViewModel()
        {
          IsChecked = copyDirectoryCommandConfig.IsChecked,
          PathToDirectory = copyDirectoryCommandConfig.PathToDirectory,
          IsCopySubfolders = copyDirectoryCommandConfig.IsCopySubfolders,
        };

      case CopyFileCommandConfig copyFileCommandConfig:
        return new CopyFileViewModel()
        {
          IsChecked = copyFileCommandConfig.IsChecked,
          PathToFile = copyFileCommandConfig.PathToFile,
        };

      case CompileAddonCommandConfig compileAddonCommandConfig:
        return new CompileAddonViewModel()
        {
          IsChecked = compileAddonCommandConfig.IsChecked,
        };

      case ClearOutputDirectoryCommandConfig clearOutputDirectoryCommandConfig:
        return new ClearOutputDirectoryViewModel()
        {
          IsChecked = clearOutputDirectoryCommandConfig.IsChecked,
        };

      default:
        throw new NotImplementedException();
    }
  }
}