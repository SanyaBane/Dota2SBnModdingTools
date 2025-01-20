using System.Windows;
using System.Windows.Controls;
using VsndevtsEditor.GUI.MainWindow.ViewModels;

namespace VsndevtsEditor.GUI.MainWindow.Views;

public partial class MainControlView : UserControl
{
  public MainControlView()
  {
    InitializeComponent();
    
    DataContextChanged += OnDataContextChanged;
  }
  
  private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
  {
    if (e.OldValue != null)
    {
      var vm = (MainControlViewModel)e.OldValue;
      // vm.ItemStateUpdated -= MainControlViewModel_OnItemStateUpdated;
    }

    if (e.NewValue != null)
    {
      var vm = (MainControlViewModel)e.NewValue;
      // vm.ItemStateUpdated += MainControlViewModel_OnItemStateUpdated;
    }
  }
}