using System.Xml.Serialization;

namespace SBnDota2ModExporter.Configs.AddonsExporter;

[Serializable]
public class CopyAddonDirectoryCommandConfig : BaseAddonExporterCommandConfig
{
  [XmlAttribute]
  public bool IsCopySubfolders { get; set; }

  public DestinationOfCopyConfigWrapper DestinationOfCopyConfigWrapper { get; set; }
}