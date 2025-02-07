using System.Xml.Serialization;

namespace RemoveCosmetics.Settings.XmlTypes;

[XmlRoot("RemoveCosmeticsConfig")]
public class RemoveCosmeticsConfigXml
{
  [XmlElement]
  public string Dota2ExeFullPath = string.Empty;

  [XmlElement]
  public string ExportVpkFileSavedDirectoryPath = string.Empty;

  [XmlArray]
  public HeroInRightListXml[] HeroesInRightList = [];

  [XmlArray]
  public PlaceholderExceptionXml[] PlaceholderFileExceptions = [];

  [XmlArray]
  public PlaceholderExceptionXml[] PlaceholderDirectoryExceptions = [];
}