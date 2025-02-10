using System.Xml.Serialization;

namespace SBnDota2ModExporter.Configs;

[Serializable]
public class SpecifiedDirectoryDestinationOfCopyDataConfig : BaseDestinationOfCopyDataConfig
{
  [XmlAttribute]
  public string RelativePathToSpecifiedDirectory { get; set; }
}