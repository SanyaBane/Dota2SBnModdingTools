using System.IO;

namespace SBnDota2ModExporter.Helpers;

public static class PathHelper
{
  public static List<DirectoryInfo> GetRelativePathDirectories(string sourceDirPath, string fullDirPath)
  {
    var pathInReverseOrder = new List<DirectoryInfo>();
    var outputDirInfo = new DirectoryInfo(fullDirPath);
    while (true)
    {
      if (outputDirInfo == null || outputDirInfo.FullName == sourceDirPath)
        break;

      pathInReverseOrder.Add(outputDirInfo);

      outputDirInfo = outputDirInfo.Parent;
    }

    return pathInReverseOrder;
  }
}