using SteamDatabase.ValvePak;

namespace CommonLib;

public static class VsndevtsInsideDota2Reader
{
  public static PackageEntry? FindFileInsideDota2(DotaAddonInfo dotaAddonInfo, FileInfo fileInsideDotaAddon, Package package)
  {
    var vsndevtsFileExtensionWithoutDot = fileInsideDotaAddon.Extension.Substring(1);
    
    var entriesVsndevtsCompiled = package.Entries.Single(x => x.Key == vsndevtsFileExtensionWithoutDot || x.Key == vsndevtsFileExtensionWithoutDot + "_c");
    
    var vsndevtsFileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInsideDotaAddon.FullName);
    var relativePathInAddonDirectoryToFile = fileInsideDotaAddon.Directory.FullName.Substring(dotaAddonInfo.DotaAddonDirectory.FullName.Length);

    if (!string.IsNullOrEmpty(relativePathInAddonDirectoryToFile))
    {
      relativePathInAddonDirectoryToFile = relativePathInAddonDirectoryToFile.Substring(1);
    }
    
    var sameVsndevtsInsidePak01DirFile = entriesVsndevtsCompiled.Value.SingleOrDefault(x => x.FileName == vsndevtsFileNameWithoutExtension && x.DirectoryName == relativePathInAddonDirectoryToFile.Replace('\\', '/'));
    return sameVsndevtsInsidePak01DirFile;
  }
}