using SBnDota2ModExporter.AddonExportCommands;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

public class CopyFileViewModel : BaseAddonExportCommandViewModel
{
  #region Fields

  private string _pathToFile = string.Empty;

  #endregion // Fields

  public CopyFileViewModel()
  {
  }

  #region Properties

  public override enAddonCommandType AddonCommandType => enAddonCommandType.CopyFile;

  public string PathToFile
  {
    get => _pathToFile;
    set
    {
      _pathToFile = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Public Methods

  public override void ApplyDataFromUpdateVm(IAddonExportCommandCreateUpdateViewModel createUpdateViewModel)
  {
    var createUpdateVm = (CopyFileCreateUpdateViewModel)createUpdateViewModel;
    PathToFile = createUpdateVm.PathToFile;
  }

  public override Task ExecuteExportCommandAsync(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress)
  {
    var copyFileCommand = new CopyFileCommand(addonOutputDirectoryFullPath, progress, PathToFile);
    copyFileCommand.Execute();
    return Task.CompletedTask;
  }

  public override IAddonExportCommandViewModel Clone()
  {
    return new CopyFileViewModel()
    {
      PathToFile = PathToFile,
      IsChecked = IsChecked,
    };
  }

  #endregion // Public Methods
}