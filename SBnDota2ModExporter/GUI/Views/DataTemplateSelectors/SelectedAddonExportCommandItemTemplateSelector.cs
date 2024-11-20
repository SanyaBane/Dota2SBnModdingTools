using System.Windows;
using System.Windows.Controls;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

namespace SBnDota2ModExporter.GUI.Views.DataTemplateSelectors;

public class SelectedAddonExportCommandItemTemplateSelector : DataTemplateSelector
{
  public DataTemplate WhenNullDataTemplate { get; set; }
  public DataTemplate CopyAddonDirectoryDataTemplate { get; set; }
  public DataTemplate CopyAddonFileDataTemplate { get; set; }
  public DataTemplate CopyDirectoryDataTemplate { get; set; }
  public DataTemplate CopyFileDataTemplate { get; set; }
  public DataTemplate CompileAddonDataTemplate { get; set; }
  public DataTemplate ClearOutputDirectoryDataTemplate { get; set; }

  public override DataTemplate SelectTemplate(object? item, DependencyObject container)
  {
    var addonExportCommandViewModel = (IAddonExportCommandViewModel)item;

    if (addonExportCommandViewModel == null)
      return new DataTemplate();

    switch (addonExportCommandViewModel)
    {
      case CopyAddonDirectoryViewModel:
        return CopyAddonDirectoryDataTemplate;
      case CopyAddonFileViewModel:
        return CopyAddonFileDataTemplate;
      case CopyDirectoryViewModel:
        return CopyDirectoryDataTemplate;
      case CopyFileViewModel:
        return CopyFileDataTemplate;
      case CompileAddonViewModel:
        return CompileAddonDataTemplate;
      case ClearOutputDirectoryViewModel:
        return ClearOutputDirectoryDataTemplate;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }
}