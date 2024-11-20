using System.IO;
using System.Text;
using SBnDota2ModExporter.AddonExportCommands;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

public class CopyAddonDirectoryViewModel : BaseAddonExportCommandViewModel
{
  #region Fields

  private string _pathToAddonDirectory = string.Empty;
  private bool _isCopySubfolders;

  #endregion // Fields

  public CopyAddonDirectoryViewModel()
  {
  }

  #region Properties

  public override enAddonCommandType AddonCommandType => enAddonCommandType.CopyAddonDirectory;

  public string PathToAddonDirectory
  {
    get => _pathToAddonDirectory;
    set
    {
      _pathToAddonDirectory = value;
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
    var createUpdateVm = (CopyAddonDirectoryCreateUpdateViewModel)createUpdateViewModel;
    PathToAddonDirectory = createUpdateVm.PathToDirectory;
    IsCopySubfolders = createUpdateVm.IsCopySubfolders;
  }

  public override Task ExecuteExportCommandAsync(string dota2AddonName, string addonOutputDirectoryFullPath, IProgress<AddonExportProgress> progress)
  {
    CopyAddonDirectoryCommand.Execute(dota2AddonName, addonOutputDirectoryFullPath, progress, PathToAddonDirectory, _isCopySubfolders);
    return Task.CompletedTask;
  }

  public override IAddonExportCommandViewModel Clone()
  {
    return new CopyAddonDirectoryViewModel()
    {
      IsCopySubfolders = IsCopySubfolders,
      PathToAddonDirectory = PathToAddonDirectory,
      IsChecked = IsChecked,
    };
  }

  #endregion // Public Methods
}