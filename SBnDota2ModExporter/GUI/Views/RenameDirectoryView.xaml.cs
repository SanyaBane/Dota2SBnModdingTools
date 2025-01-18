using System.Windows;

namespace SBnDota2ModExporter.GUI.Views;

public partial class RenameDirectoryView
{
  public RenameDirectoryView()
  {
    InitializeComponent();
    
    Loaded += OnLoaded;
  }

  private void OnLoaded(object sender, RoutedEventArgs e)
  {
    textBoxName.Focus();
    
    textBoxName.SelectionStart = 0;
    textBoxName.SelectionLength = textBoxName.Text.Length;
  }
}