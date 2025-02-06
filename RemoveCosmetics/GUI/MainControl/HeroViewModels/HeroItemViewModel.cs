using System.Windows.Media.Imaging;
using Common.WPF;

namespace RemoveCosmetics.GUI.MainControl.HeroViewModels;

public class HeroItemViewModel : BaseViewModel
{
  public required string DirectoryName { get; init; }
  public required BitmapImage? Icon { get; init; }
  
  public string HeroName => DirectoryName;
}