using System.Windows;
using System.Windows.Controls;
using VsndevtsEditor.GUI.MainWindow.ViewModels;

namespace VsndevtsEditor.GUI.MainWindow.Views.Selectors;

public class TemplateDirectoryColumnCellTemplateSelector : DataTemplateSelector
{
  public required DataTemplate Default { get; init; }
  public required DataTemplate WhenZeroActionFiles { get; init; }

  public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
  {
    var vsndevtsActionViewModel = item as VsndevtsActionViewModel;
    if (vsndevtsActionViewModel?.TemplateDirectoryData == null || vsndevtsActionViewModel.TemplateDirectoryData.FoundFiles.Length == 0)
      return WhenZeroActionFiles;

    return Default;
  }
}