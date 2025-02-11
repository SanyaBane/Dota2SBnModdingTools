﻿using System.Globalization;
using System.Windows.Data;
using CommonLib.Extensions;

namespace SBnDota2ModExporter.Converters;

public class AddonCommandTypeToTextConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    var enumVal = (Enum)value;
    var ret = EnumerationExtensions.GetEnumDescription(enumVal);
    return ret;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => null;
}