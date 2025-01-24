using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace VsndevtsEditor.Behaviors;

public static class DataGridSelectedItemsBehavior
{
  public static readonly DependencyProperty BindableSelectedItemsProperty =
    DependencyProperty.RegisterAttached(
      "BindableSelectedItems",
      typeof(IList),
      typeof(DataGridSelectedItemsBehavior),
      new PropertyMetadata(null, OnBindableSelectedItemsChanged));

  public static IList? GetBindableSelectedItems(DependencyObject obj) =>
    (IList)obj.GetValue(BindableSelectedItemsProperty);

  public static void SetBindableSelectedItems(DependencyObject obj, IList value) =>
    obj.SetValue(BindableSelectedItemsProperty, value);

  private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is DataGrid dataGrid)
    {
      dataGrid.SelectionChanged -= DataGrid_SelectionChanged;
      if (e.NewValue is IList)
      {
        dataGrid.SelectionChanged += DataGrid_SelectionChanged;
      }
    }
  }

  private static void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    if (sender is DataGrid dataGrid)
    {
      var selectedItems = GetBindableSelectedItems(dataGrid);
      if (selectedItems == null) return;

      selectedItems.Clear();
      foreach (var item in dataGrid.SelectedItems)
      {
        selectedItems.Add(item);
      }
    }
  }
}