using Common.WPF;
using SBnDota2ModExporter.Configs;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.Data;

namespace SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy;

public class DestinationOfCopyInfoViewModel : BaseViewModel
{
  #region Fields

  // private string _fullPath;
  // private enDestinationOfCopyMode _selectedDestinationOfCopyMode;

  #endregion // Fields

  #region Ctor

  public DestinationOfCopyInfoViewModel(BaseDestinationOfCopyDataConfig destinationOfCopyDataConfig)
  {
    DestinationOfCopyDataConfig = destinationOfCopyDataConfig;
  }

  #endregion // Ctor

  #region Properties

  public BaseDestinationOfCopyDataConfig DestinationOfCopyDataConfig { get; }

  public string FullPath => DestinationOfCopyDataConfig.FullPathToDirectory;

  public enDestinationOfCopyMode SelectedDestinationOfCopyMode => DestinationOfCopyDataConfig.SelectedDestinationOfCopyMode;

  #endregion // Properties

  #region Public Methods

  public DestinationOfCopyConfigWrapper CreateDestinationOfCopyConfig()
  {
    return new DestinationOfCopyConfigWrapper()
    {
      DestinationOfCopyDataConfig = DestinationOfCopyDataConfig,
    };
  }

  #endregion // Public Methods
}