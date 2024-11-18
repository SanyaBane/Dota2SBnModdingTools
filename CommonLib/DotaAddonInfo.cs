namespace CommonLib;

public class DotaAddonInfo
{
  private DotaAddonInfo()
  {
  }

  public DirectoryInfo? Dota2Directory { get; set; }
  public DirectoryInfo DotaAddonsDirectory { get; set; }
  public DirectoryInfo? DotaAddonDirectory { get; set; }
  
  public static Result<DotaAddonInfo?> GetDotaAddonInfo(FileInfo passedVsndevtsFile, FileInfo dota2Executable)
  {
    var dota2Directory = dota2Executable.Directory.Parent.Parent.Parent;
    var dotaAddonsDirectoryPath = Path.Combine(dota2Directory.FullName, "content", "dota_addons");
    var dotaAddonsDirectory = new DirectoryInfo(dotaAddonsDirectoryPath);
    
    if (dotaAddonsDirectory.Exists is false)
    {
      return new Result<DotaAddonInfo?>(false, "There was an issue when attempting to locate 'dota_addons' directory.");
    }
      
    if (passedVsndevtsFile.FullName.IndexOf(dotaAddonsDirectory.FullName, StringComparison.InvariantCultureIgnoreCase) != 0)
    {
      return new Result<DotaAddonInfo?>(false, "It looks like .vsndevts file you have passed is not located inside dota addon 'content' directory. " +
                                               "Because of it, program does not know where to look for sound files (since path to files in .vsndevts file are relative to dota addon directory).");
    }
      
    var relativePathFromDotaAddon = passedVsndevtsFile.FullName.Substring(dotaAddonsDirectory.FullName.Length);
    if (relativePathFromDotaAddon[0] != '\\')
    {
      return new Result<DotaAddonInfo?>(false, "There was an issue when attempting to locate 'dota_addons' directory.");
    }

    var dotaAddonRelativePath = relativePathFromDotaAddon.Substring(1);
    var dotaAddonName = dotaAddonRelativePath.Substring(0, dotaAddonRelativePath.IndexOf('\\'));
    var dotaAddonDirectoryPath = Path.Combine(dotaAddonsDirectory.FullName, dotaAddonName);
    var dotaAddonDirectory = new DirectoryInfo(dotaAddonDirectoryPath);

    if (dotaAddonDirectory.Exists is false)
    {
      return new Result<DotaAddonInfo?>(false, $"There was an issue when attempting to locate '{dotaAddonName}' addon directory.");
    }

    return new Result<DotaAddonInfo?>(true, new DotaAddonInfo()
    {
      Dota2Directory = dota2Directory,
      DotaAddonsDirectory = dotaAddonsDirectory,
      DotaAddonDirectory = dotaAddonDirectory,
    });
  }
}