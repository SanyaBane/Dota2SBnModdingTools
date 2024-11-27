using System.Windows;
using SBnDota2ModExporter.GUI.ViewModels;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

namespace SBnDota2ModExporter.GUI.Views;

public partial class AddonExporterInfoView
{
  public AddonExporterInfoView()
  {
    InitializeComponent();

    DataContextChanged += OnDataContextChanged;
  }

  public AddonExporterInfoViewModel ViewModel => (AddonExporterInfoViewModel)DataContext;

  private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
  {
    if (e.OldValue != null)
    {
      var vm = (AddonExporterInfoViewModel)e.OldValue;
      vm.ClickExecuteExportCommandCreate -= AddonExporterInfoViewModel_OnClickExecuteExportCommandCreate;
      vm.ClickExecuteExportCommandEdit -= AddonExporterInfoViewModel_OnClickExecuteExportCommandEdit;
    }

    if (e.NewValue != null)
    {
      var vm = (AddonExporterInfoViewModel)e.NewValue;
      vm.ClickExecuteExportCommandCreate += AddonExporterInfoViewModel_OnClickExecuteExportCommandCreate;
      vm.ClickExecuteExportCommandEdit += AddonExporterInfoViewModel_OnClickExecuteExportCommandEdit;
    }
  }

  private void AddonExporterInfoViewModel_OnClickExecuteExportCommandCreate()
  {
    var owner = Window.GetWindow(this);

    var windowCreateUpdateViewModel = new WindowCreateUpdateViewModel();

    var addonCommandCreateUpdateViewModel = new AddonCommandCreateUpdateViewModel(ViewModel.Dota2AddonName, windowCreateUpdateViewModel.CanExecuteOkCommandCallback);
    addonCommandCreateUpdateViewModel.Init();
    var addonCommandCreateUpdateView = new AddonCommandCreateUpdateView
    {
      DataContext = addonCommandCreateUpdateViewModel,
    };

    var windowCreateUpdateView = new WindowCreateUpdateView
    {
      DataContext = windowCreateUpdateViewModel,
      WindowCreateUpdateContent = addonCommandCreateUpdateView,
      Owner = owner,
      MinWidth = 600,
      MinHeight = 200,
      WindowStartupLocation = WindowStartupLocation.CenterOwner,
      SizeToContent = SizeToContent.WidthAndHeight,
    };

    if (windowCreateUpdateView.ShowDialog() == true)
    {
      ViewModel.HandleSuccessClickExecuteExportCommandCreate(addonCommandCreateUpdateViewModel.AddonExportCommandCreateUpdateViewModel);
    }
  }

  private void AddonExporterInfoViewModel_OnClickExecuteExportCommandEdit(IAddonExportCommandViewModel editVm)
  {
    var owner = Window.GetWindow(this);

    var windowCreateUpdateViewModel = new WindowCreateUpdateViewModel();

    var addonCommandCreateUpdateViewModel = new AddonCommandCreateUpdateViewModel(editVm, ViewModel.Dota2AddonName, windowCreateUpdateViewModel.CanExecuteOkCommandCallback);
    addonCommandCreateUpdateViewModel.Init();
    var addonCommandCreateUpdateView = new AddonCommandCreateUpdateView
    {
      DataContext = addonCommandCreateUpdateViewModel,
    };

    var windowCreateUpdateView = new WindowCreateUpdateView
    {
      DataContext = windowCreateUpdateViewModel,
      WindowCreateUpdateContent = addonCommandCreateUpdateView,
      Owner = owner,
      MinWidth = 600,
      MinHeight = 200,
      WindowStartupLocation = WindowStartupLocation.CenterOwner,
      SizeToContent = SizeToContent.WidthAndHeight,
    };

    if (windowCreateUpdateView.ShowDialog() == true)
    {
      ViewModel.HandleSuccessClickExecuteExportCommandEdit(editVm, addonCommandCreateUpdateViewModel.AddonExportCommandCreateUpdateViewModel);
    }
  }
}