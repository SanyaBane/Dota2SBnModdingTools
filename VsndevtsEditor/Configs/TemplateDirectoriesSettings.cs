﻿using System.IO;
using System.Xml.Serialization;

namespace VsndevtsEditor.Configs;

[XmlRoot("root")]
public class TemplateDirectoriesSettings
{
  [XmlElement("templateDirectory")]
  public required List<TemplateDirectoryData> TemplateDirectories { get; init; }

  public void UpdateFileInfosInTemplateDirectories()
  {
    var templateDirectoriesDirInfo = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Template_Directories"));
    foreach (var templateDirectory in TemplateDirectories)
    {
      var dirInfo = new DirectoryInfo(Path.Combine(templateDirectoriesDirInfo.FullName, templateDirectory.DirectoryName));
      templateDirectory.FoundFiles = dirInfo.GetFiles();
    }
  }
}

public class TemplateDirectoryData
{
  [XmlAttribute("directoryName")]
  public required string DirectoryName { get; init; }

  [XmlAttribute("scriptActionRegex")]
  public required string ScriptActionRegex { get; init; }

  [XmlIgnore]
  public FileInfo[] FoundFiles { get; set; } = [];
}
