using System.Xml.Serialization;

namespace SBnDota2ModExporter.Configs.Main;

public class AddonExporterShortConfig
{
  [XmlAttribute]
  public string FileFullPath { get; set; }

  [XmlAttribute]
  public bool IsChecked { get; set; }
}