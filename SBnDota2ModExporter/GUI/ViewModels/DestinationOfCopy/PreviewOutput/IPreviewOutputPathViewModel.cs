namespace SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.PreviewOutput;

public interface IPreviewOutputPathViewModel
{
  string OutputFullPath { get; }
  OutputTreeViewModel OutputTreeViewModel { get; }
  
  void UpdateFullPath(string dota2AddonName, AddonExportOutputInfoViewModel addonExportOutputInfoViewModel);
}