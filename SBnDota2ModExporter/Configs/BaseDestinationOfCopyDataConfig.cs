using System.Xml.Serialization;
using SBnDota2ModExporter.Enums;

namespace SBnDota2ModExporter.Configs;

[Serializable]
public abstract class BaseDestinationOfCopyDataConfig
{
  [XmlAttribute]
  public enDestinationOfCopyMode SelectedDestinationOfCopyMode { get; set; }
  
  [XmlAttribute]
  public string FullPathToDirectory { get; set; }
}