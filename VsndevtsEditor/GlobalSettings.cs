namespace VsndevtsEditor;

public class GlobalSettings
{
  #region Fields

  private string _dota2ExeFullPath;

  #endregion // Fields
  
  #region Properties

  public string Dota2ExeFullPath
  {
    get => _dota2ExeFullPath;
    set
    {
      _dota2ExeFullPath = value;
    }
  }

  #endregion // Properties
}