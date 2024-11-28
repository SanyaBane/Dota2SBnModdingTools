using SBnDota2ModExporter.Configs;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.PreviewOutput;

namespace SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.Data;

public class RelativeDestinationOfCopyDataViewModel : DefaultDestinationOfCopyDataViewModel
{
  #region Fields

  private RelativePreviewOutputPathViewModel _relativePreviewOutputPathViewModel { get; }

  #endregion // Fields

  #region Ctor

  public RelativeDestinationOfCopyDataViewModel(Func<string> getFullPathToDirectory) : base(getFullPathToDirectory)
  {
    _relativePreviewOutputPathViewModel = new RelativePreviewOutputPathViewModel(getFullPathToDirectory);
  }

  #endregion // Ctor

  #region Properties

  public override IPreviewOutputPathViewModel PreviewOutputPathViewModel => _relativePreviewOutputPathViewModel;

  #endregion // Properties

  #region Public Methods

  public override BaseDestinationOfCopyDataViewModel Clone()
  {
    return new RelativeDestinationOfCopyDataViewModel(_getFullPathToDirectory);
  }

  public override BaseDestinationOfCopyDataConfig CreateDestinationOfCopyDataConfig()
  {
    return new DefaultDestinationOfCopyDataConfig()
    {
      FullPathToDirectory = _getFullPathToDirectory.Invoke(),
      SelectedDestinationOfCopyMode = enDestinationOfCopyMode.CopyToRootUsingRelativePaths,
    };
  }

  #endregion // Public Methods
}