using System.Xml.Serialization;
using SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy;

namespace SBnDota2ModExporter.Configs;

[Serializable]
public class DestinationOfCopyConfigWrapper
{
  public DestinationOfCopyConfigWrapper()
  {
  }

  [XmlElement(nameof(DefaultDestinationOfCopyDataConfig), typeof(DefaultDestinationOfCopyDataConfig))]
  [XmlElement(nameof(SpecifiedDirectoryDestinationOfCopyDataConfig), typeof(SpecifiedDirectoryDestinationOfCopyDataConfig))]
  public object XmlDestinationOfCopyDataConfig { get; set; }

  [XmlIgnore]
  public BaseDestinationOfCopyDataConfig DestinationOfCopyDataConfig
  {
    get => (BaseDestinationOfCopyDataConfig)XmlDestinationOfCopyDataConfig;
    set => XmlDestinationOfCopyDataConfig = value;
  }

  public DestinationOfCopyCreateUpdateViewModel CreateDestinationOfCopyCreateUpdateViewModel(string dota2AddonName, AddonExportOutputInfoViewModel loadedVmAddonExportOutputInfoViewModel)
  {
    var ret = new DestinationOfCopyCreateUpdateViewModel(dota2AddonName, loadedVmAddonExportOutputInfoViewModel, DestinationOfCopyDataConfig.FullPathToDirectory, DestinationOfCopyDataConfig.SelectedDestinationOfCopyMode)
    {
      FullPathToDirectoryVm =
      {
        FullPath = DestinationOfCopyDataConfig.FullPathToDirectory
      }
    };

    return ret;
  }
}