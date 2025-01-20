using System.IO;
using VsndevtsEditor.Configs;

namespace VsndevtsEditor;

public class GlobalManager
{
  #region Singleton

  private static GlobalManager? _instance;

  public static GlobalManager Instance => _instance ??= new GlobalManager();

  #endregion // Singleton

  #region Properties

  public GlobalSettings GlobalSettings { get; } = new();

  public FolderSettings FolderSettings { get; set; }

  #endregion // Properties

  #region Private Methods

  private static string GetFullPathToExecutableDirectory()
  {
    var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
    var entryAssemblyLocation = new FileInfo(entryAssembly.Location);

    var executableDirectory = entryAssemblyLocation.Directory.FullName;
    return executableDirectory;
  }

  public string GetFullPathToConfigFile()
  {
    var executableDirectory = GetFullPathToExecutableDirectory();
    var configFileFullPath = Path.Combine(executableDirectory, Constants.CONFIG_FILE_NAME);
    return configFileFullPath;
  }

  #endregion // Private Methods
}