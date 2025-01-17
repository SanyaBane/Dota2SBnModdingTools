using CSharpFunctionalExtensions;

namespace CommonLib;

public class Dota2AddonInfo
{
  public Dota2AddonInfo(Dota2GameMainInfo dota2GameMainInfo)
  {
    Dota2GameMainInfo = dota2GameMainInfo;
  }

  #region Properties

  public Dota2GameMainInfo Dota2GameMainInfo { get; private set; }

  public DirectoryInfo? DotaAddonContentDirectoryInfo { get; private set; }
  public DirectoryInfo? DotaAddonGameDirectoryInfo { get; private set; }

  #endregion // Properties

  #region Public Methods

  public static Result<Dota2AddonInfo?> GetDotaAddonInfo(string dotaAddonName, Dota2GameMainInfo dota2GameMainInfo)
  {
    var dotaAddonContentDirectoryFullPath = Path.Combine(dota2GameMainInfo.Dota2AddonsContentDirectoryInfo.FullName, dotaAddonName);
    var dotaAddonContentDirectoryInfo = new DirectoryInfo(dotaAddonContentDirectoryFullPath);
    if (dotaAddonContentDirectoryInfo.Exists is false)
    {
      return Result.Failure<Dota2AddonInfo?>($"Directory '{dotaAddonContentDirectoryInfo.FullName}' not exists.");
    }
    
    var dotaAddonGameDirectoryFullPath = Path.Combine(dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dotaAddonName);
    var dotaAddonGameDirectoryInfo = new DirectoryInfo(dotaAddonGameDirectoryFullPath);
  
    return Result.Success<Dota2AddonInfo?>(new Dota2AddonInfo(dota2GameMainInfo)
    {
      DotaAddonContentDirectoryInfo = dotaAddonContentDirectoryInfo,
      DotaAddonGameDirectoryInfo = dotaAddonGameDirectoryInfo,
    });
  }

  public static Result<Dota2AddonInfo?> GetDotaAddonInfo(FileInfo passedVsndevtsFile, Dota2GameMainInfo dota2GameMainInfo)
  {
    if (dota2GameMainInfo.Dota2AddonsContentDirectoryInfo.Exists is false)
    {
      return Result.Failure<Dota2AddonInfo?>("There was an issue when attempting to locate 'dota_addons' directory.");
    }

    if (passedVsndevtsFile.FullName.IndexOf(dota2GameMainInfo.Dota2AddonsContentDirectoryInfo.FullName, StringComparison.InvariantCultureIgnoreCase) != 0)
    {
      return Result.Failure<Dota2AddonInfo?>($"It looks like .{ConstantsCommon.VSNDEVTS_FORMAT} file you have passed is not located inside dota addon 'content' directory. " +
                                                 $"Because of it, program does not know where to look for sound files (since path to files in .{ConstantsCommon.VSNDEVTS_FORMAT} file are relative to dota addon directory).");
    }

    var relativePathFromDotaAddon = passedVsndevtsFile.FullName.Substring(dota2GameMainInfo.Dota2AddonsContentDirectoryInfo.FullName.Length);
    if (relativePathFromDotaAddon[0] != '\\')
    {
      return Result.Failure<Dota2AddonInfo?>("There was an issue when attempting to locate 'dota_addons' directory.");
    }

    var dotaAddonRelativePath = relativePathFromDotaAddon.Substring(1);
    var dotaAddonName = dotaAddonRelativePath.Substring(0, dotaAddonRelativePath.IndexOf('\\'));
    var dotaAddonContentDirectoryPath = Path.Combine(dota2GameMainInfo.Dota2AddonsContentDirectoryInfo.FullName, dotaAddonName);
    var dotaAddonContentDirectory = new DirectoryInfo(dotaAddonContentDirectoryPath);

    if (dotaAddonContentDirectory.Exists is false)
    {
      return Result.Failure<Dota2AddonInfo?>($"There was an issue when attempting to locate '{dotaAddonName}' addon directory.");
    }
    
    var dotaAddonGameDirectoryFullPath = Path.Combine(dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dotaAddonName);
    var dotaAddonGameDirectoryInfo = new DirectoryInfo(dotaAddonGameDirectoryFullPath);

    return Result.Success<Dota2AddonInfo?>(new Dota2AddonInfo(dota2GameMainInfo)
    {
      DotaAddonContentDirectoryInfo = dotaAddonContentDirectory,
      DotaAddonGameDirectoryInfo = dotaAddonGameDirectoryInfo,
    });
  }

  #endregion // Public Methods
}