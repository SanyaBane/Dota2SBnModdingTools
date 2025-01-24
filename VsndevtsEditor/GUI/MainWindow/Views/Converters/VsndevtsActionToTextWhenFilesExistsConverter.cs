using System.Globalization;
using System.Windows.Data;
using VsndevtsEditor.Configs;

namespace VsndevtsEditor.GUI.MainWindow.Views.Converters;

public class VsndevtsActionToTextWhenFilesExistsConverter : IValueConverter
{
  public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
  {
    var templateDirectoryData = value as TemplateDirectoryData;
    if (templateDirectoryData == null)
      return null;

    return $"On auto-populate will use '{templateDirectoryData.DirectoryName}' directory ({templateDirectoryData.FoundFiles.Length} files)";
  }

  public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}