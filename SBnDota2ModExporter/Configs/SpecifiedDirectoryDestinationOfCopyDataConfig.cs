namespace SBnDota2ModExporter.Configs;

[Serializable]
public class SpecifiedDirectoryDestinationOfCopyDataConfig : BaseDestinationOfCopyDataConfig
{
  public string RelativePathToSpecifiedDirectory { get; set; }
}