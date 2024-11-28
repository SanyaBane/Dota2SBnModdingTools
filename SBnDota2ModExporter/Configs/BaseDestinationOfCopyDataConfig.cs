using SBnDota2ModExporter.Enums;

namespace SBnDota2ModExporter.Configs;

[Serializable]
public abstract class BaseDestinationOfCopyDataConfig
{
  public enDestinationOfCopyMode SelectedDestinationOfCopyMode { get; set; }
  
  public string FullPathToDirectory { get; set; }
}