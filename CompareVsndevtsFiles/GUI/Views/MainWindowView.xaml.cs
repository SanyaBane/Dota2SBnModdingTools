using System.Windows;
using CompareVsndevtsFiles.GUI.ViewModels;

namespace CompareVsndevtsFiles.GUI.Views;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
    
    var mainWindowViewModel = new MainWindowViewModel();
    DataContext = mainWindowViewModel;
  }
}