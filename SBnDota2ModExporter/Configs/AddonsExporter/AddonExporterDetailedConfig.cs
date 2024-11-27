using System.Xml.Serialization;

namespace SBnDota2ModExporter.Configs.AddonsExporter;

public class AddonExporterDetailedConfig
{
  public AddonExporterDetailedConfig()
  {
  }

  [XmlElement]
  public string? Dota2AddonName { get; set; }

  [XmlElement]
  public string? AddonOutputDirectoryName { get; set; }

  [XmlArray("AddonExporterCommandConfigs")]
  [XmlArrayItem("AddonExporterCommandConfig")]
  public List<AddonExporterCommandConfigWrapper> AddonExporterCommandConfigWrappers { get; } = new();
}