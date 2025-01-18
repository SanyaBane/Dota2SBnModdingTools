using System.ComponentModel;

namespace SBnDota2ModExporter.Enums;

public enum enDestinationOfCopyMode
{
  [Description("Copy to addon output directory")]
  CopyToRoot,
  [Description("Copy to addon output directory, by same path as inside source addon (relative path)")]
  CopyToRootUsingRelativePaths,
  [Description("Copy to addon output directory, but manually specify sub-directory")]
  CopyToSpecifiedDirectory,
}