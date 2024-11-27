using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SBnDota2ModExporter.GUI.Views.DataTemplateSelectors;

public class AddonInfoContentTemplateSelector : DataTemplateSelector
{
  public DataTemplate FileNotSaved { get; set; }
  public DataTemplate FileIsSaved { get; set; }

  public override DataTemplate SelectTemplate(object? item, DependencyObject container)
  {
    var addonConfigFileInfo = item as FileInfo;
    return addonConfigFileInfo == null ? FileNotSaved : FileIsSaved;
  }
}