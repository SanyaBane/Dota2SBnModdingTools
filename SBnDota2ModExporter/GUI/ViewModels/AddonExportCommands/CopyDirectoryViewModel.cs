using SBnDota2ModExporter.AddonExportCommands;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

public class CopyDirectoryViewModel : BaseAddonExportCommandViewModel
{
  #region Fields

  private string _pathToDirectory = string.Empty;
  private bool _isCopySubfolders;
  private bool _isCopyOnlyContentOfDirectory;

  #endregion // Fields

  public CopyDirectoryViewModel()
  {
  }

  #region Properties

  public override enAddonCommandType AddonCommandType => enAddonCommandType.CopyDirectory;

  public string PathToDirectory
  {
    get => _pathToDirectory;
    set
    {
      _pathToDirectory = value;
      OnPropertyChanged();
    }
  }

  public bool IsCopySubfolders
  {
    get => _isCopySubfolders;
    set
    {
      _isCopySubfolders = value;
      OnPropertyChanged();
    }
  }

  public bool IsCopyOnlyContentOfDirectory
  {
    get => _isCopyOnlyContentOfDirectory;
    set
    {
      _isCopyOnlyContentOfDirectory = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Public Methods

  public override void ApplyDataFromUpdateVm(IAddonExportCommandCreateUpdateViewModel createUpdateViewModel)
  {
    var createUpdateVm = (CopyDirectoryCreateUpdateViewModel)createUpdateViewModel;
    PathToDirectory = createUpdateVm.PathToDirectory;
    IsCopySubfolders = createUpdateVm.IsCopySubfolders;
    IsCopyOnlyContentOfDirectory = createUpdateVm.IsCopyOnlyContentOfDirectory;
  }

  public override async Task ExecuteExportCommandAsync(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress)
  {
    var copyDirectoryCommand = new CopyDirectoryCommand(addonOutputDirectoryFullPath, progress, PathToDirectory, IsCopySubfolders, IsCopyOnlyContentOfDirectory);
    await copyDirectoryCommand.Execute();
  }

  public override IAddonExportCommandViewModel Clone()
  {
    return new CopyDirectoryViewModel()
    {
      IsChecked = IsChecked,
      PathToDirectory = PathToDirectory,
      IsCopySubfolders = IsCopySubfolders,
      IsCopyOnlyContentOfDirectory = IsCopyOnlyContentOfDirectory,
    };
  }

  #endregion // Public Methods
}