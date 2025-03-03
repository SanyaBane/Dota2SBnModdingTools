using SteamDatabase.ValvePak;

namespace CommonLib;

public static class VsndevtsInsideDota2Reader
{
  public static PackageEntry? FindFileInsideDota2(Dota2AddonInfo dota2AddonInfo, FileInfo fileInsideDotaAddon, Package package)
  {
    var vsndevtsFileExtensionWithoutDot = fileInsideDotaAddon.Extension.Substring(1);

    if (!package.Entries.Any(x => x.Key == vsndevtsFileExtensionWithoutDot || x.Key == vsndevtsFileExtensionWithoutDot + "_c"))
      return null;
    
    var entriesVsndevtsCompiled = package.Entries.Single(x => x.Key == vsndevtsFileExtensionWithoutDot || x.Key == vsndevtsFileExtensionWithoutDot + "_c");
    
    var vsndevtsFileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInsideDotaAddon.FullName);
    var relativePathInAddonDirectoryToFile = fileInsideDotaAddon.Directory.FullName.Substring(dota2AddonInfo.DotaAddonContentDirectoryInfo.FullName.Length);

    if (!string.IsNullOrEmpty(relativePathInAddonDirectoryToFile))
    {
      relativePathInAddonDirectoryToFile = relativePathInAddonDirectoryToFile.Substring(1);
    }
    
    var sameVsndevtsInsidePak01DirFile = entriesVsndevtsCompiled.Value.SingleOrDefault(x => x.FileName == vsndevtsFileNameWithoutExtension && x.DirectoryName == relativePathInAddonDirectoryToFile.Replace('\\', '/'));
    return sameVsndevtsInsidePak01DirFile;
  }
}