using Common.WPF;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;
using SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

public class CopyAddonDirectoryCreateUpdateViewModel : BaseViewModel, IAddonExportCommandCreateUpdateViewModel
{
  #region Fields

  private readonly AddonExportOutputInfoViewModel _addonExportOutputInfoViewModel;
  private readonly Action<bool>? _canExecuteOkCommandCallback;
  private readonly CopyAddonDirectoryViewModel? _editVm;

  private bool _isCopySubfolders = true;
  private bool _isDirty;

  #endregion // Fields

  #region Ctor

  public CopyAddonDirectoryCreateUpdateViewModel(string dota2AddonName, AddonExportOutputInfoViewModel addonExportOutputInfoViewModel, Action<bool>? canExecuteOkCommandCallback, CopyAddonDirectoryViewModel? editVm)
  {
    _addonExportOutputInfoViewModel = addonExportOutputInfoViewModel;
    _canExecuteOkCommandCallback = canExecuteOkCommandCallback;
    _editVm = editVm;

    if (editVm != null)
    {
      _isCopySubfolders = editVm.IsCopySubfolders;
      // DestinationOfCopyCreateUpdateViewModel = editVm.DestinationOfCopyInfoViewModel.Clone();
      DestinationOfCopyCreateUpdateViewModel = new DestinationOfCopyCreateUpdateViewModel(dota2AddonName, _addonExportOutputInfoViewModel, editVm.DestinationOfCopyInfoViewModel.FullPath, editVm.DestinationOfCopyInfoViewModel.SelectedDestinationOfCopyMode);
      DestinationOfCopyCreateUpdateViewModel.DestinationOfCopyDataViewModel.PreviewOutputPathViewModel.UpdateFullPath(dota2AddonName, _addonExportOutputInfoViewModel);
    }
    else
    {
      DestinationOfCopyCreateUpdateViewModel = new DestinationOfCopyCreateUpdateViewModel(dota2AddonName, _addonExportOutputInfoViewModel, string.Empty, enDestinationOfCopyMode.CopyToRootUsingRelativePaths);
    }

    DestinationOfCopyCreateUpdateViewModel.IsDirtyChange += () => { IsDirty = true; };
  }

  #endregion // Ctor

  #region Properties

  public bool IsCreatingVm => _editVm == null;
  public bool IsUpdatingVm => !IsCreatingVm;

  public DestinationOfCopyCreateUpdateViewModel DestinationOfCopyCreateUpdateViewModel { get; }

  public bool IsCopySubfolders
  {
    get => _isCopySubfolders;
    set
    {
      _isCopySubfolders = value;
      OnPropertyChanged();

      IsDirty = true;
    }
  }

  private bool IsDirty
  {
    get => _isDirty;
    set
    {
      _isDirty = value;
      OnPropertyChanged();

      RefreshCommands();
    }
  }

  #endregion // Properties

  #region Public Methods

  public override void RefreshCommands()
  {
    base.RefreshCommands();

    _canExecuteOkCommandCallback.Invoke(CanExecuteOkCommand());
  }

  #endregion // Public Methods

  #region Protected Methods

  protected override void OnTokenChanged()
  {
    base.OnTokenChanged();

    DestinationOfCopyCreateUpdateViewModel.Token = Token;
  }

  #endregion // Protected Methods

  #region Private Methods

  private bool CanExecuteOkCommand()
  {
    return IsDirty && DestinationOfCopyCreateUpdateViewModel.IsValidViewModel();
  }

  #endregion // Private Methods
}