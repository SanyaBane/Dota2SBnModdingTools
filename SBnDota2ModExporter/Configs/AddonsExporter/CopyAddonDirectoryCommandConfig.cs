namespace SBnDota2ModExporter.Configs.AddonsExporter;

[Serializable]
public class CopyAddonDirectoryCommandConfig : BaseAddonExporterCommandConfig
{
  public bool IsCopySubfolders { get; set; }
  public DestinationOfCopyConfigWrapper DestinationOfCopyConfigWrapper { get; set; }
}