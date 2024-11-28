using System.Windows;
using System.Windows.Controls;
using SBnDota2ModExporter.Enums;

namespace SBnDota2ModExporter.GUI.Views.DataTemplateSelectors;

public class DestinationOfCopyContentTemplateSelector : DataTemplateSelector
{
  public DataTemplate CopyToRootDataTemplate { get; set; }
  public DataTemplate CopyToRootUsingRelativePathsDataTemplate { get; set; }
  public DataTemplate CopyToSpecifiedDirectoryDataTemplate { get; set; }

  public override DataTemplate SelectTemplate(object? item, DependencyObject container)
  {
    if (item == null)
      return base.SelectTemplate(item, container);

    var destinationOfCopyMode = (enDestinationOfCopyMode)item;

    switch (destinationOfCopyMode)
    {
      case enDestinationOfCopyMode.CopyToRoot:
        return CopyToRootDataTemplate;
      case enDestinationOfCopyMode.CopyToRootUsingRelativePaths:
        return CopyToRootUsingRelativePathsDataTemplate;
      case enDestinationOfCopyMode.CopyToSpecifiedDirectory:
        return CopyToSpecifiedDirectoryDataTemplate;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }
}