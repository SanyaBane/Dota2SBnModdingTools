using SBnDota2ModExporter.AddonExportCommands;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;
using SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

public class CopyAddonDirectoryViewModel : BaseAddonExportCommandViewModel
{
  #region Fields

  private string _pathToAddonDirectory = string.Empty;
  private bool _isCopySubfolders;
  private DestinationOfCopyInfoViewModel _destinationOfCopyInfoViewModel;

  #endregion // Fields

  public CopyAddonDirectoryViewModel()
  {
  }

  #region Properties

  public override enAddonCommandType AddonCommandType => enAddonCommandType.CopyAddonDirectory;

  public bool IsCopySubfolders
  {
    get => _isCopySubfolders;
    set
    {
      _isCopySubfolders = value;
      OnPropertyChanged();
    }
  }

  public DestinationOfCopyInfoViewModel DestinationOfCopyInfoViewModel
  {
    get => _destinationOfCopyInfoViewModel;
    set
    {
      _destinationOfCopyInfoViewModel = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Public Methods

  public override void ApplyDataFromUpdateVm(IAddonExportCommandCreateUpdateViewModel createUpdateViewModel)
  {
    var createUpdateVm = (CopyAddonDirectoryCreateUpdateViewModel)createUpdateViewModel;
    IsCopySubfolders = createUpdateVm.IsCopySubfolders;
    // DestinationOfCopyInfoViewModel = createUpdateVm.DestinationOfCopyCreateUpdateViewModel.Clone();
    DestinationOfCopyInfoViewModel = new DestinationOfCopyInfoViewModel(createUpdateVm.DestinationOfCopyCreateUpdateViewModel.DestinationOfCopyDataViewModel.CreateDestinationOfCopyDataConfig());
  }

  public override Task ExecuteExportCommandAsync(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress)
  {
    var copyAddonDirectoryCommand = new CopyAddonDirectoryCommand(dota2AddonName, addonOutputDirectoryFullPath, progress, _destinationOfCopyInfoViewModel.FullPath, _destinationOfCopyInfoViewModel.SelectedDestinationOfCopyMode, _isCopySubfolders);
    copyAddonDirectoryCommand.Execute();
    return Task.CompletedTask;
  }

  public override IAddonExportCommandViewModel Clone()
  {
    return new CopyAddonDirectoryViewModel()
    {
      IsCopySubfolders = IsCopySubfolders,
      DestinationOfCopyInfoViewModel = new DestinationOfCopyInfoViewModel(DestinationOfCopyInfoViewModel.DestinationOfCopyDataConfig),
      // DestinationOfCopyInfoViewModel = DestinationOfCopyInfoViewModel.Clone(),
      IsChecked = IsChecked,
    };
  }

  #endregion // Public Methods
}