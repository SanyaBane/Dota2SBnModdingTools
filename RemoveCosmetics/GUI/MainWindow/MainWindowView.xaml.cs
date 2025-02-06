using System.Windows;

namespace RemoveCosmetics.GUI.MainWindow;

public partial class MainWindowView
{
  public MainWindowView()
  {
    InitializeComponent();
    Closed += OnClosed;
  }
  
  private void OnClosed(object? sender, EventArgs e)
  {
    Application.Current.Shutdown();
  }
}