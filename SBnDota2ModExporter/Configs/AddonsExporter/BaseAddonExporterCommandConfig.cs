using System.Xml.Serialization;

namespace SBnDota2ModExporter.Configs.AddonsExporter;

public abstract class BaseAddonExporterCommandConfig
{
  [XmlAttribute]
  public bool IsChecked { get; set; }
}