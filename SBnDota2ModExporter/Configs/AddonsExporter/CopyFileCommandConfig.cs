using System.Xml.Serialization;

namespace SBnDota2ModExporter.Configs.AddonsExporter;

[Serializable]
public class CopyFileCommandConfig : BaseAddonExporterCommandConfig
{
  [XmlAttribute]
  public string PathToFile { get; set; }
}