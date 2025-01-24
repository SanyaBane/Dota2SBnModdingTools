using System.IO;

namespace VsndevtsEditor.Models;

public class TemplateDirectoryFiles
{
  public required string DirectoryName { get; init; }

  public List<FileInfo> FilesInfos { get; } = new List<FileInfo>();
}