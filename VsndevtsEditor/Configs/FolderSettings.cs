using System.Xml.Serialization;

namespace VsndevtsEditor.Configs;

[XmlRoot("root")]
public class FolderSettings
{
  [XmlElement("folder")]
  public required List<Folder> Folders { get; init; }
}

public class Folder
{
  [XmlAttribute("folderName")]
  public required string FolderName { get; init; }

  [XmlAttribute("scriptAction")]
  public required string ScriptAction { get; init; }
}
