using SBnDota2ModExporter.Configs;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.PreviewOutput;

namespace SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.Data;

public class DefaultDestinationOfCopyDataViewModel : BaseDestinationOfCopyDataViewModel
{
  #region Fields

  protected readonly Func<string> _getFullPathToDirectory;

  private readonly DefaultPreviewOutputPathViewModel _defaultPreviewOutputPathViewModel;

  #endregion // Fields

  #region Ctor

  public DefaultDestinationOfCopyDataViewModel(Func<string> getFullPathToDirectory)
  {
    _getFullPathToDirectory = getFullPathToDirectory;
    _defaultPreviewOutputPathViewModel = new DefaultPreviewOutputPathViewModel(getFullPathToDirectory);
  }

  #endregion // Ctor

  #region Properties

  public override IPreviewOutputPathViewModel PreviewOutputPathViewModel => _defaultPreviewOutputPathViewModel;

  #endregion // Properties

  #region Public Methods

  public override BaseDestinationOfCopyDataViewModel Clone()
  {
    return new DefaultDestinationOfCopyDataViewModel(_getFullPathToDirectory);
  }

  public override BaseDestinationOfCopyDataConfig CreateDestinationOfCopyDataConfig()
  {
    return new DefaultDestinationOfCopyDataConfig()
    {
      FullPathToDirectory = _getFullPathToDirectory.Invoke(), 
      SelectedDestinationOfCopyMode = enDestinationOfCopyMode.CopyToRoot,
    };
  }

  #endregion // Public Methods
}