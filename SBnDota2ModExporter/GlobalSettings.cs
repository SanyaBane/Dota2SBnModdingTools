namespace SBnDota2ModExporter;

public class GlobalSettings
{
  #region Fields

  private string _dota2ExeFullPath;
  private string _outputDirectoryFullPath;

  #endregion // Fields

  #region Events

  // public event Action? Dota2ExeFullPathChange;
  public event Action? OutputDirectoryFullPathChange;

  #endregion // Events

  #region Properties

  public string Dota2ExeFullPath
  {
    get => _dota2ExeFullPath;
    set
    {
      _dota2ExeFullPath = value;
      // Dota2ExeFullPathChange?.Invoke();
    }
  }

  public string OutputDirectoryFullPath
  {
    get => _outputDirectoryFullPath;
    set
    {
      _outputDirectoryFullPath = value;
      OutputDirectoryFullPathChange?.Invoke();
    }
  }

  #endregion // Properties
}