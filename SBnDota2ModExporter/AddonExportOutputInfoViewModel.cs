using System.IO;
using Common.WPF;

namespace SBnDota2ModExporter;

public class AddonExportOutputInfoViewModel : BaseViewModel
{
  public enum enAddonOutputDirectoryStatus
  {
    None,
    GlobalOutputDirectoryNotSet,
    AddonDirectoryNotSet
  }
  
  #region Fields

  private readonly Func<string> _getDota2AddonName;

  private enAddonOutputDirectoryStatus _addonOutputDirectoryStatus = enAddonOutputDirectoryStatus.None;
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

  public enAddonOutputDirectoryStatus AddonOutputDirectoryStatus
  {
    get => _addonOutputDirectoryStatus;
    private set
    {
      _addonOutputDirectoryStatus = value;
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
    AddonOutputDirectoryFullPath = string.Empty;
    
    if (string.IsNullOrEmpty(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath))
    {
      AddonOutputDirectoryStatus = enAddonOutputDirectoryStatus.GlobalOutputDirectoryNotSet;
      return;
    }

    var dota2AddonName = _getDota2AddonName.Invoke();
    if (string.IsNullOrEmpty(dota2AddonName))
    {
      AddonOutputDirectoryStatus = enAddonOutputDirectoryStatus.AddonDirectoryNotSet;
      return;
    }

    AddonOutputDirectoryStatus = enAddonOutputDirectoryStatus.None;
    AddonOutputDirectoryFullPath = Path.Combine(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath,
      string.IsNullOrEmpty(CustomOutputDirectoryName)
        ? dota2AddonName
        : CustomOutputDirectoryName);
  }

  #endregion // Public Methods
}