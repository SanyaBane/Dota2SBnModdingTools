using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels;

namespace SBnDota2ModExporter.Configs;

[Serializable]
public class DestinationOfCopyConfig
{
  public DestinationOfCopyConfig()
  {
  }

  public enDestinationOfCopyMode SelectedDestinationOfCopyMode;

  public DestinationOfCopyViewModel CreateDestinationOfCopyViewModel()
  {
    return new DestinationOfCopyViewModel()
    {
      SelectedDestinationOfCopyMode = SelectedDestinationOfCopyMode
    };
  }
}