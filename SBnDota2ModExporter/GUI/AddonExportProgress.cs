using System.Windows.Media;

namespace SBnDota2ModExporter.GUI;

public class AddonExportProgress
{
  public AddonExportProgress(string text)
  {
    Text = text;
  }

  public AddonExportProgress(string text, Brush foregroundColor) : this(text)
  {
    ForegroundColor = foregroundColor;
  }

  public string Text { get; }

  public Brush ForegroundColor { get; } = Brushes.Black;
}