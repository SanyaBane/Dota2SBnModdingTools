using Common.WPF;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

public class ClearOutputDirectoryCreateUpdateViewModel : BaseViewModel, IAddonExportCommandCreateUpdateViewModel
{
  #region Fields

  private readonly string _dota2AddonName;
  private readonly Action<bool>? _canExecuteOkCommandCallback;

  #endregion // Fields

  #region Ctor

  public ClearOutputDirectoryCreateUpdateViewModel(string dota2AddonName, Action<bool>? canExecuteOkCommandCallback)
  {
    _dota2AddonName = dota2AddonName;
    _canExecuteOkCommandCallback = canExecuteOkCommandCallback;

    _canExecuteOkCommandCallback.Invoke(CanExecuteOkCommand());
  }

  #endregion // Ctor

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