namespace SBnDota2ModExporter.Configs.AddonsExporter;

[Serializable]
public class CopyAddonDirectoryCommandConfig : BaseAddonExporterCommandConfig
{
  public string PathToDirectory { get; set; }
  public bool IsCopySubfolders { get; set; }
  
  public DestinationOfCopyConfig DestinationOfCopyConfig { get; set; }
}