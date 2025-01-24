using System.IO;

namespace VsndevtsEditor;

public class Constants
{
  public static string PathToTemplateDirectoriesSettingsFile => Path.Combine(Environment.CurrentDirectory, "Settings", "TemplateDirectoriesSettings.xml");
  
  public const string CONFIG_FILE_NAME = "VsndevtsEditor.config";
  
  public const string VSNDEVTS_FILE_FORMAT = "vsndevts";
}