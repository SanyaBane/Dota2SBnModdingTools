using System.Globalization;
using System.Windows.Data;
using CommonLib.Extensions;

namespace Common.WPF.Converters;

public class EnumToDescriptionConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value == null)
      return null;

    var enumVal = (Enum)value;
    var ret = EnumerationExtensions.GetEnumDescription(enumVal);
    return ret;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return null;
  }
}