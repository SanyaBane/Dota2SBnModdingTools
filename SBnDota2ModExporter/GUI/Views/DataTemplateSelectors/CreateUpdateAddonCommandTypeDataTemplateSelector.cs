using System.Windows;
using System.Windows.Controls;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.Views.DataTemplateSelectors;

public class CreateUpdateAddonCommandTypeDataTemplateSelector : DataTemplateSelector
{
  public DataTemplate CopyAddonDirectoryDataTemplate { get; set; }
  public DataTemplate CopyAddonFileDataTemplate { get; set; }
  public DataTemplate CopyDirectoryDataTemplate { get; set; }
  public DataTemplate CopyFileDataTemplate { get; set; }
  public DataTemplate CompileAddonDataTemplate { get; set; }
  public DataTemplate ClearOutputDirectoryDataTemplate { get; set; }

  public override DataTemplate SelectTemplate(object item, DependencyObject container)
  {
    var addonExportCommandsViewModel = (IAddonExportCommandCreateUpdateViewModel)item;
    switch (addonExportCommandsViewModel)
    {
      case CopyAddonDirectoryCreateUpdateViewModel:
        return CopyAddonDirectoryDataTemplate;
      case CopyAddonFileCreateUpdateViewModel:
        return CopyAddonFileDataTemplate;
      case CopyDirectoryCreateUpdateViewModel:
        return CopyDirectoryDataTemplate;
      case CopyFileCreateUpdateViewModel:
        return CopyFileDataTemplate;
      case CompileAddonCreateUpdateViewModel:
        return CompileAddonDataTemplate;
      case ClearOutputDirectoryCreateUpdateViewModel:
        return ClearOutputDirectoryDataTemplate;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }
}