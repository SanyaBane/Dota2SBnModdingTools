using System.Windows;
using System.Windows.Controls;
using SBnDota2ModExporter.GUI.ViewModels;

namespace SBnDota2ModExporter.GUI.Views.DataTemplateSelectors;

public class SelectedAddonExporterFileDataTemplateSelector : DataTemplateSelector
{
  public DataTemplate WhenItemNotSelected { get; set; }
  public DataTemplate WhenItemSelected { get; set; }
  
  public override DataTemplate SelectTemplate(object item, DependencyObject container)
  {
    var addonExporterInfoViewModel = item as AddonExporterInfoViewModel;
    if (addonExporterInfoViewModel == null)
      return WhenItemNotSelected;
    
    return WhenItemSelected;
  }
}