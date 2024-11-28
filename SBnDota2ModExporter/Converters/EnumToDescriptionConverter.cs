using System.Globalization;
using System.Windows.Data;

namespace SBnDota2ModExporter.Converters;

public class EnumToDescriptionConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value == null)
      return null;

    var enumVal = (Enum)value;
    var ret = Enumerations.GetEnumDescription(enumVal);
    return ret;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return null;
  }
}