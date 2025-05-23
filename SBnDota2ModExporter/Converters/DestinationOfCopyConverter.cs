﻿using System.Globalization;
using System.Windows.Data;

namespace SBnDota2ModExporter.Converters;

public class DestinationOfCopyConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return value?.Equals(parameter);
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    return value?.Equals(true) == true ? parameter : Binding.DoNothing;
  }
}