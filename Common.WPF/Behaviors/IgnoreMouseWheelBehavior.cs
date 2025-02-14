using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace Common.WPF.Behaviors;

/// <summary>
/// Captures and eats MouseWheel events so that a nested ListBox does not
/// prevent an outer scrollable control from scrolling.
/// https://stackoverflow.com/a/7003338/3867255
/// </summary>
public sealed class IgnoreMouseWheelBehavior : Behavior<UIElement>
{
  protected override void OnAttached()
  {
    base.OnAttached();
    AssociatedObject.PreviewMouseWheel += AssociatedObject_PreviewMouseWheel;
  }

  protected override void OnDetaching()
  {
    AssociatedObject.PreviewMouseWheel -= AssociatedObject_PreviewMouseWheel;
    base.OnDetaching();
  }

  void AssociatedObject_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
  {
    e.Handled = true;

    var e2 = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
    e2.RoutedEvent = UIElement.MouseWheelEvent;

    AssociatedObject.RaiseEvent(e2);
  }
}