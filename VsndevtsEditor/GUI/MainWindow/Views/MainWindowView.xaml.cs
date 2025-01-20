using System.Windows;

namespace VsndevtsEditor.GUI.MainWindow.Views;

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