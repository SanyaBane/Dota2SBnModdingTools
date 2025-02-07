using System.Xml.Serialization;

namespace RemoveCosmetics.Settings.XmlTypes;

[XmlType("PlaceholderException")]
public class PlaceholderExceptionXml
{
  [XmlAttribute]
  public bool IsRegexPattern { get; init; } = false;

  [XmlAttribute]
  public string Value { get; init; } = string.Empty;
}