using System.Xml.Serialization;

namespace RemoveCosmetics.Settings.XmlTypes;

[XmlRoot("RemoveCosmeticsConfig")]
public class RemoveCosmeticsConfigXml
{
  [XmlElement]
  public string Dota2ExeFullPath = string.Empty;

  [XmlElement]
  public string PlaceholderVpkFileDirectoryFullPath = string.Empty;

  [XmlArray]
  public HeroInRightListXml[] HeroesInRightList = [];

  [XmlArray]
  public PlaceholderDirectoryExceptionXml[] PlaceholderDirectoryExceptions = [];

  [XmlArray]
  public PlaceholderFileExceptionXml[] PlaceholderFileExceptions = [];
}