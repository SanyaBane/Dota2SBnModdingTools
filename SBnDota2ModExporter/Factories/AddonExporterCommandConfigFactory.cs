using SBnDota2ModExporter.Configs.AddonsExporter;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

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
          PathToDirectory = copyDirectoryViewModel.PathToAddonDirectory,
          IsCopySubfolders = copyDirectoryViewModel.IsCopySubfolders,
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

  public static IAddonExportCommandViewModel CreateAddonExportCommandViewModel(BaseAddonExporterCommandConfig addonExporterCommandConfig)
  {
    switch (addonExporterCommandConfig)
    {
      case CopyAddonDirectoryCommandConfig copyDirectoryCommandConfig:
        return new CopyAddonDirectoryViewModel()
        {
          IsChecked = copyDirectoryCommandConfig.IsChecked,
          PathToAddonDirectory = copyDirectoryCommandConfig.PathToDirectory,
          IsCopySubfolders = copyDirectoryCommandConfig.IsCopySubfolders,
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