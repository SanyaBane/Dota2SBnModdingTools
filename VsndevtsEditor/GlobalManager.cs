using VsndevtsEditor.Configs;

namespace VsndevtsEditor;

public class GlobalManager
{
  #region Singleton

  private static GlobalManager? _instance;

  public static GlobalManager Instance => _instance ??= new GlobalManager();

  #endregion // Singleton

  #region Properties

  public TemplateDirectoriesSettings TemplateDirectoriesSettings { get; set; }

  #endregion // Properties
}