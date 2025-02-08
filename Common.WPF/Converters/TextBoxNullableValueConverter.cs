using System.Windows.Data;

namespace Common.WPF.Converters;

public class TextBoxNullableValueConverter : IValueConverter
{
  public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
  {
    return value;
  }

  public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
  {
    var strValue = (string)value;

    if (string.IsNullOrWhiteSpace(strValue))
      return null;

    return value;
  }
}