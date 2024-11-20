using System.Windows;

namespace SBnDota2ModExporter.GUI.Views;

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