using System.Windows.Media;

namespace SBnDota2ModExporter;

public static class Constants
{
  public const string ADDON_EXPORT_FILE_FORMAT = "d2adexp";
  
  public const string SKIP_COMMAND_TEXT = "Skip command.";

  public static readonly Brush RTB_FOREGROUND_COLOR_SUCCESS = Brushes.Green;
  public static readonly Brush RTB_FOREGROUND_COLOR_WARNING = Brushes.Yellow;
  public static readonly Brush RTB_FOREGROUND_COLOR_ANOTHER_PROGRAM = Brushes.DimGray;
  public static readonly Brush RTB_FOREGROUND_COLOR_ERROR = Brushes.Red;
}