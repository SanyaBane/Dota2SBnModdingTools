using System.Windows;
using System.Windows.Data;

namespace SBnDota2ModExporter.Converters;

[ValueConversion(typeof(bool), typeof(Visibility), ParameterType = typeof(bool))]
public class BooleanToVisibilityConverter : IValueConverter
{
  public Visibility VisibilityWhenFalse { get; set; } = Visibility.Collapsed;

  public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
  {
    var isInverse = parameter != null && (bool)parameter;
    var target = (bool)value;

    if (isInverse)
    {
      target = !target;
    }

    return target ? Visibility.Visible : VisibilityWhenFalse;
  }

  public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}