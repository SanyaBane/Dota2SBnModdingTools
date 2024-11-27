using System.IO;
using Common.WPF;
using SBnDota2ModExporter.Configs;
using SBnDota2ModExporter.Enums;

namespace SBnDota2ModExporter.GUI.ViewModels;

public class DestinationOfCopyViewModel : BaseViewModel
{
  #region Fields

  private enDestinationOfCopyMode _selectedDestinationOfCopyMode = enDestinationOfCopyMode.CopyToRootUsingRelativePaths;

  #endregion // Fields

  #region Events

  public event Action? SelectedDestinationOfCopyChange;

  #endregion // Events

  #region Properties

  public enDestinationOfCopyMode SelectedDestinationOfCopyMode
  {
    get => _selectedDestinationOfCopyMode;
    set
    {
      _selectedDestinationOfCopyMode = value;
      OnPropertyChanged();

      SelectedDestinationOfCopyChange?.Invoke();
    }
  }

  public string? PreviewResult { get; private set; }

  #endregion // Properties

  #region Public Methods

  public void UpdateVmAfterPathToDirectoryChange(PathToDirectoryViewModel pathToDirectoryVm, string dota2AddonName)
  {
    if (string.IsNullOrEmpty(pathToDirectoryVm.FullPath))
    {
      PreviewResult = string.Empty;
    }
    else
    {
      var dir = new DirectoryInfo(pathToDirectoryVm.FullPath);

      switch (SelectedDestinationOfCopyMode)
      {
        case enDestinationOfCopyMode.CopyToRoot:
          PreviewResult = Path.Combine(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath, dota2AddonName, dir.Name);
          break;
        case enDestinationOfCopyMode.CopyToRootUsingRelativePaths:
          var addonGameDirectoryFullPath = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dota2AddonName);
          if (dir.FullName.StartsWith(addonGameDirectoryFullPath, StringComparison.InvariantCultureIgnoreCase))
          {
            var newPath = dir.FullName.Substring(addonGameDirectoryFullPath.Length);
            if (newPath[0] == '\\')
            {
              newPath = newPath.Substring(1);
            }

            PreviewResult = Path.Combine(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath, dota2AddonName, newPath);
          }
          else
          {
            throw new Exception();
          }

          break;
        // case enDestinationOfCopy.CopyToSpecifiedDirectory:
        //   // todo
        //   PreviewResult = Path.Combine(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath, dota2AddonName, dir.Name);
        //   break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    OnPropertyChanged(nameof(PreviewResult));
  }

  public DestinationOfCopyViewModel Clone()
  {
    return new DestinationOfCopyViewModel()
    {
      SelectedDestinationOfCopyMode = SelectedDestinationOfCopyMode,
    };
  }

  public DestinationOfCopyConfig CreateDestinationOfCopyConfig()
  {
    return new DestinationOfCopyConfig()
    {
      SelectedDestinationOfCopyMode = SelectedDestinationOfCopyMode
    };
  }

  #endregion // Public Methods
}