namespace RemoveCosmetics.Settings;

public class RemoveCosmeticsConfig
{
  private string _dota2ExeFullPath = string.Empty;
  private string _exportVpkFileSavedDirectoryPath = string.Empty;
  private string[] _heroesInRightList = [];

  public bool IsDirty { get; private set; }

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
}