namespace SBnDota2ModExporter.Configs.AddonsExporter;

[Serializable]
public class CopyAddonFileCommandConfig : BaseAddonExporterCommandConfig
{
  public string PathToFile { get; set; }
}