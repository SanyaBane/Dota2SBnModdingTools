using System.IO;
using System.Xml.Serialization;
using CommonLib;
using CSharpFunctionalExtensions;

namespace VsndevtsEditor.Configs;

public class VsndevtsEditorGlobalConfig
{
  [XmlElement]
  public string Dota2ExeFullPath { get; set; }
  
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