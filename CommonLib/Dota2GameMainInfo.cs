namespace CommonLib;

public class Dota2GameMainInfo
{
  private Dota2GameMainInfo()
  {
  }

  #region Properties

  public FileInfo Dota2ExecutableFileInfo { get; private init; }
  public FileInfo ResourceCompilerExecutableFileInfo { get; private init; }
  public DirectoryInfo Dota2DirectoryInfo { get; private init; }
  public FileInfo Pak01DirVpkFileInfo { get; private init; }
  public DirectoryInfo Dota2AddonsContentDirectoryInfo { get; private init; }
  public DirectoryInfo Dota2AddonsGameDirectoryInfo { get; private init; }

  #endregion // Properties

  #region Public Methods

  public static Result<Dota2GameMainInfo?> CreateDota2GameMainInfo(string fullPathToDota2Exe)
  {
    var dota2ExecutableFile = new FileInfo(fullPathToDota2Exe);
    if (dota2ExecutableFile.Exists is false)
      return new Result<Dota2GameMainInfo?>($"File not found by following path:{Environment.NewLine}" +
                                            $"{fullPathToDota2Exe}");

    var resourceCompilerExecutableFile = new FileInfo(Path.Combine(dota2ExecutableFile.Directory.FullName, "resourcecompiler.exe"));

    var dota2Directory = dota2ExecutableFile.Directory.Parent.Parent.Parent;

    string pak01DirFullPath = Path.Combine(dota2Directory.FullName, "game", "dota", "pak01_dir.vpk");
    var pak01DirVpkFile = new FileInfo(pak01DirFullPath);
    if (pak01DirVpkFile.Exists is false)
    {
      return new Result<Dota2GameMainInfo?>($"Can not find 'pak01_dir.vpk' file by following path: {pak01DirFullPath}");
    }

    var dotaAddonsContentDirectoryPath = Path.Combine(dota2Directory.FullName, "content", "dota_addons");
    var dotaAddonsContentDirectoryInfo = new DirectoryInfo(dotaAddonsContentDirectoryPath);
    if (dotaAddonsContentDirectoryInfo.Exists is false)
    {
      return new Result<Dota2GameMainInfo?>($"Can not find 'dota_addons' content directory by following path: {dotaAddonsContentDirectoryPath}");
    }
    
    var dotaAddonsGameDirectoryPath = Path.Combine(dota2Directory.FullName, "game", "dota_addons");
    var dotaAddonsGameDirectoryInfo = new DirectoryInfo(dotaAddonsGameDirectoryPath);

    var dota2GameMainInfo = new Dota2GameMainInfo()
    {
      Dota2ExecutableFileInfo = dota2ExecutableFile,
      ResourceCompilerExecutableFileInfo = resourceCompilerExecutableFile,
      Dota2DirectoryInfo = dota2Directory,
      Pak01DirVpkFileInfo = pak01DirVpkFile,
      Dota2AddonsContentDirectoryInfo = dotaAddonsContentDirectoryInfo,
      Dota2AddonsGameDirectoryInfo = dotaAddonsGameDirectoryInfo,
    };

    return new Result<Dota2GameMainInfo?>(true, dota2GameMainInfo);
  }

  #endregion // Public Methods
}