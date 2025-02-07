namespace RemoveCosmetics.Settings.Types;

public class RemoveCosmeticsConfig
{
  #region Fields

  private string _dota2ExeFullPath = string.Empty;
  private string _exportVpkFileSavedDirectoryPath = string.Empty;
  private string[] _heroesInRightList = [];
  private PlaceholderException[] _placeholderDirectoryExceptions = [];
  private PlaceholderException[] _placeholderFileExceptions = [];

  #endregion // Fields

  #region Ctor

  public RemoveCosmeticsConfig()
  {
  }

  #endregion // Ctor

  #region Properties

  public bool IsDirty { get; set; }

  public string Dota2ExeFullPath
  {
    get => _dota2ExeFullPath;
    set
    {
      _dota2ExeFullPath = value;

      IsDirty = true;
    }
  }

  public string ExportVpkFileSavedDirectoryPath
  {
    get => _exportVpkFileSavedDirectoryPath;
    set
    {
      _exportVpkFileSavedDirectoryPath = value;

      IsDirty = true;
    }
  }

  public string[] HeroesInRightList
  {
    get => _heroesInRightList;
    set
    {
      _heroesInRightList = value;

      IsDirty = true;
    }
  }

  public PlaceholderException[] PlaceholderDirectoryExceptions
  {
    get => _placeholderDirectoryExceptions;
    set
    {
      _placeholderDirectoryExceptions = value;

      IsDirty = true;
    }
  }

  public PlaceholderException[] PlaceholderFileExceptions
  {
    get => _placeholderFileExceptions;
    set
    {
      _placeholderFileExceptions = value;

      IsDirty = true;
    }
  }

  #endregion // Properties
}