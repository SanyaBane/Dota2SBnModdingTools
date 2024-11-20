using Common.WPF;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

public abstract class BaseAddonExportCommandViewModel : BaseViewModel, IAddonExportCommandViewModel
{
  #region Fields

  private bool _isChecked;
  private int _index;

  #endregion // Fields

  #region Events

  public event Action? IsCheckedChange;

  #endregion // Events

  #region Properties

  public abstract enAddonCommandType AddonCommandType { get; }

  public string Name => Enumerations.GetEnumDescription(AddonCommandType);

  public bool IsChecked
  {
    get => _isChecked;
    set
    {
      _isChecked = value;
      OnPropertyChanged();

      IsCheckedChange?.Invoke();
    }
  }

  public int Index
  {
    get => _index;
    set
    {
      _index = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Public Methods

  public abstract void ApplyDataFromUpdateVm(IAddonExportCommandCreateUpdateViewModel createUpdateViewModel);
  public abstract Task ExecuteExportCommandAsync(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress);

  public abstract IAddonExportCommandViewModel Clone();

  #endregion // Public Methods
}