using System.Globalization;
using System.Windows.Data;
using VsndevtsEditor.Configs;

namespace VsndevtsEditor.GUI.MainWindow.Views.Converters;

public class VsndevtsActionToTextWhenFilesNotExistsConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    var templateDirectoryData = value as TemplateDirectoryData;
    if (templateDirectoryData == null)
      return null;

    return $"Template directory '{templateDirectoryData.DirectoryName}' doesn't have files inside of it";
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}