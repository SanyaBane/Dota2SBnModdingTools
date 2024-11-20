using System.Xml.Serialization;

namespace SBnDota2ModExporter.Configs.Main;

public class AddonExporterShortConfig
{
  [XmlElement]
  public string FileFullPath { get; set; }

  [XmlElement]
  public bool IsChecked { get; set; }
}