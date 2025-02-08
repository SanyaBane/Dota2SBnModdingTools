using System.Globalization;
using System.Windows.Data;

namespace RemoveCosmetics.GUI.MainControl.Converters;

public class FullPathToExecutableDirectoryToTextConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    if (value == null)
      return null;

    var fullPathToExecutableDirectory = (string)value;
    if (fullPathToExecutableDirectory.Length <= 30)
      return null;
    
    return $"Path to program is too long ({fullPathToExecutableDirectory.Length} characters):{Environment.NewLine}" +
           $"{fullPathToExecutableDirectory}{Environment.NewLine}{Environment.NewLine}" +
           $"It is recommended to move the program directory to the root of your drive (for example, \"D:\\RemoveCosmeticsDota2\\\") " +
           $"to avoid possible problems with creating placeholders, since some Dota2 items have very long names, and the VPK creator does not support files longer than 255 characters.";
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}