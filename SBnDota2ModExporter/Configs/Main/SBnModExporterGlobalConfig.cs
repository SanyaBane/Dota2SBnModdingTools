using System.IO;
using System.Xml.Serialization;
using CommonLib;
using CSharpFunctionalExtensions;

namespace SBnDota2ModExporter.Configs.Main;

public class SBnModExporterGlobalConfig
{
  [XmlElement]
  public string Dota2ExeFullPath { get; set; } = string.Empty;
  
  [XmlElement]
  public string OutputDirectoryFullPath { get; set; } = string.Empty;

  [XmlArray]
  public List<AddonExporterShortConfig> AddonExporterShortConfigs { get; set; } = new();
}