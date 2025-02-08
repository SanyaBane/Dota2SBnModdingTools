using System.Windows;
using System.Windows.Data;

namespace Common.WPF.Converters;

public class NullToVisibilityConverter : IValueConverter
{
  #region Properties

  public bool Inverse { get; set; }

  public bool TreatEmptyStringAsNull { get; set; }

  #endregion // Properties

  public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
  {
    if (value is string str && TreatEmptyStringAsNull)
      return Inverse ^ !string.IsNullOrEmpty(str) ? Visibility.Visible : Visibility.Collapsed;

    return Inverse ^ value != null ? Visibility.Visible : Visibility.Collapsed;
  }

  public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
  {
    return null;
  }
}