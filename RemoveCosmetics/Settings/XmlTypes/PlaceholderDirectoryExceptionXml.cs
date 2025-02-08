using System.Xml.Serialization;

namespace RemoveCosmetics.Settings.XmlTypes;

[XmlType("PlaceholderDirectoryException")]
public class PlaceholderDirectoryExceptionXml
{
  [XmlAttribute]
  public bool IsRegexPattern { get; init; } = false;

  [XmlAttribute]
  public string Value { get; init; } = string.Empty;

  [XmlAttribute]
  public string? Comment { get; init; }
}