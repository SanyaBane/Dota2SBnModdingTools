using System.IO;
using System.Xml.Serialization;
using CommonLib;
using CSharpFunctionalExtensions;

namespace SBnDota2ModExporter.Configs.Main;

public class SBnModExporterGlobalConfig
{
  [XmlElement]
  public string Dota2ExeFullPath { get; set; }
  
  [XmlElement]
  public string OutputDirectoryFullPath { get; set; }

  [XmlArray]
  public List<AddonExporterShortConfig> AddonExporterShortConfigs { get; set; } = new();
  
  public Result TrySaveConfigFile()
  {
    try
    {
      var fullPathToConfigFile = GlobalManager.Instance.GetFullPathToConfigFile();
      XmlSerializerService.SerializeToXml(fullPathToConfigFile, this);

      if (File.Exists(fullPathToConfigFile) is false)
      {
        return Result.Failure($"Was not able to save config file by following path:{Environment.NewLine}" + 
                          $"{fullPathToConfigFile}");
      }

      return Result.Success();
    }
    catch (Exception ex)
    {
      return Result.Failure(ex.Message);
    }
  }
}