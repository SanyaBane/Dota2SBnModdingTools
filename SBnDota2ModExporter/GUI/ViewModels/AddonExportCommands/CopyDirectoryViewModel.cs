using SBnDota2ModExporter.AddonExportCommands;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

public class CopyDirectoryViewModel : BaseAddonExportCommandViewModel
{
  #region Fields

  private string _pathToDirectory = string.Empty;
  private bool _isCopySubfolders;

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

  #endregion // Properties

  #region Public Methods

  public override void ApplyDataFromUpdateVm(IAddonExportCommandCreateUpdateViewModel createUpdateViewModel)
  {
    var createUpdateVm = (CopyDirectoryCreateUpdateViewModel)createUpdateViewModel;
    PathToDirectory = createUpdateVm.PathToDirectory;
    IsCopySubfolders = createUpdateVm.IsCopySubfolders;
  }

  public override Task ExecuteExportCommandAsync(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress)
  {
    CopyDirectoryCommand.Execute(addonOutputDirectoryFullPath, progress, PathToDirectory, IsCopySubfolders);
    return Task.CompletedTask;
  }

  public override IAddonExportCommandViewModel Clone()
  {
    return new CopyDirectoryViewModel()
    {
      IsCopySubfolders = IsCopySubfolders,
      PathToDirectory = PathToDirectory,
      IsChecked = IsChecked,
    };
  }

  #endregion // Public Methods
}