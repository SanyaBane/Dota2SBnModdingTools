using System.Windows;
using System.Windows.Media;

namespace RemoveCosmetics.GUI.MainControl.PlaceholderCreation;

public class PlaceholderCreationProgress(string text)
{
  public PlaceholderCreationProgress(string text, Brush foregroundColor) : this(text)
  {
    ForegroundColor = foregroundColor;
  }

  public PlaceholderCreationProgress(string text, Brush foregroundColor, FontWeight fontWeight) : this(text, foregroundColor)
  {
    FontWeight = fontWeight;
  }

  public string Text { get; } = text;

  public Brush ForegroundColor { get; } = Brushes.Black;
  public FontWeight FontWeight { get; } = FontWeights.Normal;
}