using System.Globalization;
using System.Windows.Data;
using VsndevtsEditor.Configs;
using VsndevtsEditor.GUI.MainWindow.ViewModels;

namespace VsndevtsEditor.GUI.MainWindow.Views.Converters;

public class VsndevtsActionToTextWhenTemplateDirNotExistsConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    var vsndevtsActionViewModel = value as VsndevtsActionViewModel;
    if (vsndevtsActionViewModel == null)
      return null;

    return $"No template directory found for action '{vsndevtsActionViewModel.ActionName}'";
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}