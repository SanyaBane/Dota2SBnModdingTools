using System.Windows;
using System.Windows.Controls;
using VsndevtsEditor.GUI.MainWindow.ViewModels;

namespace VsndevtsEditor.GUI.MainWindow.Views.Selectors;

public class TemplateDirectoryColumnCellTemplateSelector : DataTemplateSelector
{
  public required DataTemplate WhenTemplateDirectoryNotExists { get; init; }
  public required DataTemplate WhenActionFilesExists { get; init; }
  public required DataTemplate WhenActionFilesNotExists { get; init; }

  public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
  {
    var vsndevtsActionViewModel = item as VsndevtsActionViewModel;
    
    if (vsndevtsActionViewModel?.TemplateDirectoryData == null)
      return WhenTemplateDirectoryNotExists;
    
    if (vsndevtsActionViewModel.TemplateDirectoryData.FoundFiles.Length == 0)
      return WhenActionFilesNotExists;

    return WhenActionFilesExists;
  }
}