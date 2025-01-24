using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Messaging;
using VsndevtsEditor.GUI.MainWindow.Messages;
using VsndevtsEditor.GUI.MainWindow.ViewModels;

namespace VsndevtsEditor.GUI.MainWindow.Views;

public partial class MainControlView : UserControl
{
  public MainControlView()
  {
    InitializeComponent();

    DataContextChanged += OnDataContextChanged;

    WeakReferenceMessenger.Default.Register<CallAreYouSureWindowMessage>(this, AreYouSureMessageHandler);
  }

  private MainControlViewModel _mainControlViewModel;

  private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
  {
    if (e.NewValue != null)
    {
      _mainControlViewModel = (MainControlViewModel)e.NewValue;
    }
  }

  private void AreYouSureMessageHandler(object recipient, CallAreYouSureWindowMessage windowMessage)
  {
    var wnd = Window.GetWindow(this);
    if (MessageBox.Show(wnd,
          "Are you sure you want to assign 'null' for ALL actions?",
          "Confirm action", MessageBoxButton.YesNo, MessageBoxImage.Question)
        == MessageBoxResult.Yes)
    {
      _mainControlViewModel.SetActionsToNull(_mainControlViewModel.ActionViewModels);
    }
  }
}