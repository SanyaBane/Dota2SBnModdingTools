using Common.WPF;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

public class ClearOutputDirectoryCreateUpdateViewModel : BaseViewModel, IAddonExportCommandCreateUpdateViewModel
{
  #region Fields

  private readonly string _dota2AddonName;
  private readonly Action<bool>? _canExecuteOkCommandCallback;
  private readonly ClearOutputDirectoryViewModel? _editVm;

  #endregion // Fields

  #region Ctor

  public ClearOutputDirectoryCreateUpdateViewModel(string dota2AddonName, Action<bool>? canExecuteOkCommandCallback, ClearOutputDirectoryViewModel? editVm)
  {
    _dota2AddonName = dota2AddonName;
    _canExecuteOkCommandCallback = canExecuteOkCommandCallback;
    _editVm = editVm;

    _canExecuteOkCommandCallback.Invoke(CanExecuteOkCommand());
  }

  #endregion // Ctor

  #region Properties

  public bool IsCreatingVm => _editVm == null;
  public bool IsUpdatingVm => !IsCreatingVm;

  #endregion // Properties

  #region Public Methods

  public override void RefreshCommands()
  {
    base.RefreshCommands();

    _canExecuteOkCommandCallback.Invoke(CanExecuteOkCommand());
  }

  #endregion // Public Methods

  #region Private Methods

  private bool CanExecuteOkCommand() => true;

  #endregion // Private Methods
}