using System.Windows;
using System.Windows.Media;

namespace RemoveCosmetics.GUI.MainControl.PlaceholderCreation;

public class PlaceholderCreationProgress()
{
  public required string Text { get; init; }
  public Brush ForegroundColor { get; init; } = Brushes.Black;
  public FontWeight FontWeight { get; init; } = FontWeights.Normal;
}