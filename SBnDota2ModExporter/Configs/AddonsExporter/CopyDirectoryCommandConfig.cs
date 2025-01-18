namespace SBnDota2ModExporter.Configs.AddonsExporter;

[Serializable]
public class CopyDirectoryCommandConfig : BaseAddonExporterCommandConfig
{
  public string PathToDirectory { get; set; }
  public bool IsCopySubfolders { get; set; }
  public bool IsCopyOnlyContentOfDirectory { get; set; }
}