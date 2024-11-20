using System.Windows;
using SBnDota2ModExporter.GUI.ViewModels;

namespace SBnDota2ModExporter.GUI.Views;

public partial class WindowCreateUpdateView
{
  public static readonly DependencyProperty WindowCreateUpdateContentProperty = DependencyProperty.Register(nameof(WindowCreateUpdateContent), typeof(object), typeof(WindowCreateUpdateView));
  
  public object WindowCreateUpdateContent
  {
    get => GetValue(WindowCreateUpdateContentProperty);
    set => SetValue(WindowCreateUpdateContentProperty, value);
  }
  
  public WindowCreateUpdateView()
  {
    InitializeComponent();
    
    DataContextChanged += OnDataContextChanged;
  }

  private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
  {
    if (e.OldValue != null)
    {
      var vm = (WindowCreateUpdateViewModel)e.OldValue;
      vm.OkButtonPress -= WindowCreateUpdateViewModel_OnOkButtonPress;
    }
    
    if (e.NewValue != null)
    {
      var vm = (WindowCreateUpdateViewModel)e.NewValue;
      vm.OkButtonPress += WindowCreateUpdateViewModel_OnOkButtonPress;
    }
  }

  private void WindowCreateUpdateViewModel_OnOkButtonPress()
  {
    DialogResult = true;
    Close();
  }
}