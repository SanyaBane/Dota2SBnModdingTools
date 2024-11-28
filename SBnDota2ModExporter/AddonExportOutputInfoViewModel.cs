using System.IO;
using Common.WPF;

namespace SBnDota2ModExporter;

public class AddonExportOutputInfoViewModel : BaseViewModel
{
  #region Fields

  private readonly Func<string> _getDota2AddonName;

  private string _customOutputDirectoryName = string.Empty;
  private string _addonOutputDirectoryFullPath = string.Empty;
  private bool _isDirty;

  #endregion // Fields

  #region Ctor

  public AddonExportOutputInfoViewModel(Func<string> getDota2AddonName)
  {
    _getDota2AddonName = getDota2AddonName;
  }

  #endregion // Ctor

  #region Events

  public event Action? IsDirtyChange;
  
  #endregion // Events

  #region Properties

  public string CustomOutputDirectoryName
  {
    get => _customOutputDirectoryName;
    set
    {
      _customOutputDirectoryName = value;
      OnPropertyChanged();

      UpdateAddonOutputDirectory();

      IsDirty = true;
    }
  }

  public string AddonOutputDirectoryFullPath
  {
    get => _addonOutputDirectoryFullPath;
    private set
    {
      _addonOutputDirectoryFullPath = value;
      OnPropertyChanged();
    }
  }

  public bool IsDirty
  {
    get => _isDirty;
    private set
    {
      _isDirty = value;
      OnPropertyChanged();

      IsDirtyChange?.Invoke();
    }
  }

  #endregion // Properties

  #region Public Methods

  public AddonExportOutputInfoViewModel Clone()
  {
    return new AddonExportOutputInfoViewModel(_getDota2AddonName)
    {
      CustomOutputDirectoryName = CustomOutputDirectoryName,
      IsDirty = false,
    };
  }

  public void UpdateAddonOutputDirectory()
  {
    var dota2AddonName = _getDota2AddonName.Invoke();
    if (string.IsNullOrEmpty(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath))
    {
      AddonOutputDirectoryFullPath = "*SET GLOBAL OUTPUT DIRECTORY FIRST*";
      return;
    }

    if (string.IsNullOrEmpty(dota2AddonName))
    {
      AddonOutputDirectoryFullPath = "*SET ADDON DIRECTORY FIRST*";
      return;
    }

    AddonOutputDirectoryFullPath = Path.Combine(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath,
      string.IsNullOrEmpty(CustomOutputDirectoryName)
        ? dota2AddonName
        : CustomOutputDirectoryName);
  }

  #endregion // Public Methods
}