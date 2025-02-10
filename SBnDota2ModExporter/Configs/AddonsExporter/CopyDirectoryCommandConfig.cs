using System.Xml.Serialization;

namespace SBnDota2ModExporter.Configs.AddonsExporter;

[Serializable]
public class CopyDirectoryCommandConfig : BaseAddonExporterCommandConfig
{
  [XmlAttribute]
  public string PathToDirectory { get; set; }

  [XmlAttribute]
  public bool IsCopySubfolders { get; set; }

  [XmlAttribute]
  public bool IsCopyOnlyContentOfDirectory { get; set; }
}