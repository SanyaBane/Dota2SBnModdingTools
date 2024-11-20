using Common.WPF;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.ViewModels;

public class AddonCommandCreateUpdateViewModel : BaseViewModel
{
  #region Fields

  private readonly string _dota2AddonName;
  private readonly Action<bool>? _canExecuteOkCommandCallback;

  private enAddonCommandType _selectedAddonCommandType;
  private IAddonExportCommandCreateUpdateViewModel _addonExportCommandCreateUpdateViewModel;

  private readonly Dictionary<enAddonCommandType, IAddonExportCommandCreateUpdateViewModel> _createdViewModels = new();

  private readonly IAddonExportCommandViewModel? _editVm;

  #endregion // Fields

  #region Ctor

  public AddonCommandCreateUpdateViewModel(string dota2AddonName, Action<bool>? canExecuteOkCommandCallback)
  {
    _dota2AddonName = dota2AddonName;
    _canExecuteOkCommandCallback = canExecuteOkCommandCallback;

    AddonCommandTypes = Enum.GetValues(typeof(enAddonCommandType))
      .Cast<enAddonCommandType>()
      .Except(new[] { enAddonCommandType.CopyAddonFile }) // what the point of "CopyAddonFile" anyway?
      .OrderBy(x => Enumerations.GetEnumDescription(x))
      .ToArray();

    _selectedAddonCommandType = AddonCommandTypes.First();
  }

  public AddonCommandCreateUpdateViewModel(IAddonExportCommandViewModel editVm, string dota2AddonName, Action<bool>? canExecuteOkCommandCallback) : this(dota2AddonName, canExecuteOkCommandCallback)
  {
    _editVm = editVm;

    _selectedAddonCommandType = editVm.AddonCommandType;

    var instance = CreateCreateUpdateViewModelInstance(_selectedAddonCommandType);
    _createdViewModels[_selectedAddonCommandType] = instance;
  }

  #endregion // Ctor

  #region Properties

  public bool IsCreatingVm => _editVm == null;
  public bool IsUpdatingVm => !IsCreatingVm;

  public enAddonCommandType[] AddonCommandTypes { get; }

  public enAddonCommandType SelectedAddonCommandType
  {
    get => _selectedAddonCommandType;
    set
    {
      _selectedAddonCommandType = value;
      OnPropertyChanged();

      UpdateAddonExportCommandsViewModel();

      _createdViewModels[_selectedAddonCommandType].RefreshCommands();
    }
  }

  public IAddonExportCommandCreateUpdateViewModel AddonExportCommandCreateUpdateViewModel
  {
    get => _addonExportCommandCreateUpdateViewModel;
    private set
    {
      _addonExportCommandCreateUpdateViewModel = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Public Methods

  public void Init()
  {
    UpdateAddonExportCommandsViewModel();
  }

  #endregion // Public Methods

  #region Private Methods

  private void UpdateAddonExportCommandsViewModel()
  {
    if (_createdViewModels.TryGetValue(_selectedAddonCommandType, out var viewModel))
    {
      AddonExportCommandCreateUpdateViewModel = viewModel;
    }
    else
    {
      var instance = CreateCreateUpdateViewModelInstance(_selectedAddonCommandType);
      _createdViewModels[_selectedAddonCommandType] = instance;

      AddonExportCommandCreateUpdateViewModel = instance;
    }
  }

  private IAddonExportCommandCreateUpdateViewModel CreateCreateUpdateViewModelInstance(enAddonCommandType addonCommandType)
  {
    switch (addonCommandType)
    {
      case enAddonCommandType.CopyAddonDirectory:
        return new CopyAddonDirectoryCreateUpdateViewModel(_dota2AddonName, _canExecuteOkCommandCallback, _editVm as CopyAddonDirectoryViewModel);
      case enAddonCommandType.CopyAddonFile:
        return new CopyAddonFileCreateUpdateViewModel(_dota2AddonName, _canExecuteOkCommandCallback, _editVm as CopyAddonFileViewModel);
      case enAddonCommandType.CopyDirectory:
        return new CopyDirectoryCreateUpdateViewModel(_dota2AddonName, _canExecuteOkCommandCallback, _editVm as CopyDirectoryViewModel);
      case enAddonCommandType.CopyFile:
        return new CopyFileCreateUpdateViewModel(_dota2AddonName, _canExecuteOkCommandCallback, _editVm as CopyFileViewModel);
      case enAddonCommandType.CompileAddon:
        return new CompileAddonCreateUpdateViewModel(_dota2AddonName, _canExecuteOkCommandCallback);
      case enAddonCommandType.ClearOutputDirectory:
        return new ClearOutputDirectoryCreateUpdateViewModel(_dota2AddonName, _canExecuteOkCommandCallback);
      default:
        throw new ArgumentOutOfRangeException(nameof(addonCommandType), addonCommandType, null);
    }
  }

  #endregion // Private Methods
}