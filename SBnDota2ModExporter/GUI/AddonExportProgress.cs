using System.Windows;
using System.Windows.Media;

namespace SBnDota2ModExporter.GUI;

public class AddonExportProgress(string text)
{
  public AddonExportProgress(string text, Brush foregroundColor) : this(text)
  {
    ForegroundColor = foregroundColor;
  }

  public AddonExportProgress(string text, Brush foregroundColor, FontWeight fontWeight) : this(text, foregroundColor)
  {
    FontWeight = fontWeight;
  }

  public string Text { get; } = text;

  public Brush ForegroundColor { get; } = Brushes.Black;
  public FontWeight FontWeight { get; } = FontWeights.Normal;
}