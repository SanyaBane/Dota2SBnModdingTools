using System.Globalization;
using System.Windows;
using System.Windows.Data;
using RemoveCosmetics.Constants;

namespace RemoveCosmetics.GUI.MainControl.Converters;

public class FullPathToExecutableDirectoryToVisibilityConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value == null)
      return null;

    var fullPathToExecutableDirectory = (string)value;
    return fullPathToExecutableDirectory.Length <= ConstantsGeneral.RECOMMENDED_LENGTH_OF_PATH_TO_EXE_FILE_LIMIT ? Visibility.Collapsed : Visibility.Visible;
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}