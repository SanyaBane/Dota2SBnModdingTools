using System.Globalization;
using System.Windows.Data;
using VsndevtsEditor.Configs;

namespace VsndevtsEditor.GUI.MainWindow.Views.Converters;

public class TemplateDirectoryDataToListOfFilesConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    var templateDirectoryData = value as TemplateDirectoryData;
    if (templateDirectoryData == null)
      return string.Empty;

    var allFileNames = templateDirectoryData.FoundFiles.Select(x => $"\"{x.Name}\"");
    return string.Join(", ", allFileNames);
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}