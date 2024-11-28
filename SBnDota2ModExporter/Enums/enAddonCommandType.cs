using System.ComponentModel;

namespace SBnDota2ModExporter.Enums;

public enum enAddonCommandType
{
  [Description("Compile Addon")]
  CompileAddon,

  [Description("Clear Output Directory")]
  ClearOutputDirectory,

  [Description("Copy Addon Directory")]
  CopyAddonDirectory,

  [Description("Copy Addon File")]
  CopyAddonFile,

  [Description("Copy Directory")]
  CopyDirectory,

  [Description("Copy File")]
  CopyFile,
}