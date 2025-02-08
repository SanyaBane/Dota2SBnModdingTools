using System.Xml.Serialization;

namespace RemoveCosmetics.Settings.XmlTypes;

[XmlType("PlaceholderFileException")]
public class PlaceholderFileExceptionXml
{
  [XmlAttribute]
  public bool IsRegexPattern { get; init; } = false;

  [XmlAttribute]
  public string Value { get; init; } = string.Empty;

  [XmlAttribute]
  public string? Comment { get; init; }
}