using System.Windows;

namespace SBnDota2ModExporter.GUI.Behaviors;

public static class ControlBehaviors
{
  #region Is Show On Disabled

  public static readonly DependencyProperty IsShowOnDisabledProperty = DependencyProperty.RegisterAttached("IsShowOnDisabled", 
    typeof(bool), typeof(ControlBehaviors), new FrameworkPropertyMetadata(true, OnIsShowOnDisabledPropertyChangedCallback));

  [AttachedPropertyBrowsableForType(typeof(UIElement))]
  public static bool GetIsShowOnDisabled(UIElement uiElement)
  {
    return (bool)uiElement.GetValue(IsShowOnDisabledProperty);
  }

  public static void SetIsShowOnDisabled(UIElement uiElement, bool isShowOnDisabled)
  {
    uiElement.SetValue(IsShowOnDisabledProperty, isShowOnDisabled);
  }

  private static void OnIsShowOnDisabledPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
  {
    var uiElement = dependencyObject as UIElement;

    if (uiElement == null)
      return;

    uiElement.IsEnabledChanged -= OnUiElementIsEnabledChanged;

    if ((bool)args.NewValue)
    {
      uiElement.Visibility = Visibility.Visible;
    }
    else
    {
      uiElement.Visibility = uiElement.IsEnabled ? Visibility.Visible : Visibility.Collapsed;
      uiElement.IsEnabledChanged += OnUiElementIsEnabledChanged;
    }
  }

  private static void OnUiElementIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs args)
  {
    var uiElelement = sender as UIElement;

    if (uiElelement == null)
      return;

    uiElelement.Visibility = (bool)args.NewValue ? Visibility.Visible : Visibility.Collapsed;
  }

  #endregion // Is Show On Disabled
}