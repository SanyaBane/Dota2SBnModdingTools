﻿using System.Windows.Data;

namespace SBnDota2ModExporter.Converters;

public class BooleanInvertConverter : IValueConverter
{
  public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
  {
    return !(bool)value;
  }

  public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}