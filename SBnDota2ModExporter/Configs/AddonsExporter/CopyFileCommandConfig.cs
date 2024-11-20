namespace SBnDota2ModExporter.Configs.AddonsExporter;

[Serializable]
public class CopyFileCommandConfig : BaseAddonExporterCommandConfig
{
  public string PathToFile { get; set; }
}