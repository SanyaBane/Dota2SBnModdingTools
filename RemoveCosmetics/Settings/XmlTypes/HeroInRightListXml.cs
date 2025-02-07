using System.Xml.Serialization;

namespace RemoveCosmetics.Settings.XmlTypes;

[XmlType("Hero")]
public class HeroInRightListXml
{
  [XmlAttribute]
  public string Value { get; set; } = string.Empty;
}