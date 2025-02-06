using System.Xml.Serialization;

namespace RemoveCosmetics.Settings;

[XmlRoot("RemoveCosmeticsConfig")]
public class RemoveCosmeticsConfigXml
{
  [XmlElement]
  public string Dota2ExeFullPath = string.Empty;

  [XmlElement]
  public string OutputDirectoryFullPath = string.Empty;
  
  [XmlArray]
  public HeroInRightList[] HeroesInRightList = [];

  [XmlType("Hero")]
  public class HeroInRightList
  {
    [XmlAttribute]
    public string Value { get; set; } = string.Empty;
  }
}