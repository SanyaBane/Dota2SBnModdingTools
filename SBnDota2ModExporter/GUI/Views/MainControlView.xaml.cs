using System.Windows;
using SBnDota2ModExporter.GUI.ViewModels;

namespace SBnDota2ModExporter.GUI.Views;

public partial class MainControlView
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
      vm.ItemStateUpdated -= MainControlViewModel_OnItemStateUpdated;
    }

    if (e.NewValue != null)
    {
      var vm = (MainControlViewModel)e.NewValue;
      vm.ItemStateUpdated += MainControlViewModel_OnItemStateUpdated;
    }
  }

  private void MainControlViewModel_OnItemStateUpdated(AddonExporterInfoViewModel vm)
  {
    dgLoadedAddonExportFiles.ScrollIntoView(vm);
  }
}