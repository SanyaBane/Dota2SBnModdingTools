namespace RemoveCosmetics.Helpers;

public class VersionHelper
{
  private static Version? _version;
  public static Version Version
  {
    get
    {
      if (_version != null) 
        return _version;
      
      var assembly = System.Reflection.Assembly.GetExecutingAssembly();
      _version = assembly.GetName().Version;

      return _version!;
    }
  }
}