using System.IO;
using SBnDota2ModExporter.Helpers;

namespace SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.PreviewOutput;

public class RelativePreviewOutputPathViewModel : DefaultPreviewOutputPathViewModel
{
  #region Fields

  private string _commonPartOfPath = string.Empty;
  private string _relativePath = string.Empty;

  #endregion // Fields

  #region Ctor

  public RelativePreviewOutputPathViewModel(Func<string> getFullPathToDirectory) : base(getFullPathToDirectory)
  {
  }

  #endregion // Ctor

  #region Properties

  public string CommonPartOfPath
  {
    get => _commonPartOfPath;
    private set
    {
      _commonPartOfPath = value;
      OnPropertyChanged();
    }
  }

  public string RelativePath
  {
    get => _relativePath;
    private set
    {
      _relativePath = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Public Methods

  public override void UpdateFullPath(string dota2AddonName, AddonExportOutputInfoViewModel addonExportOutputInfoViewModel)
  {
    ResetPreviewOutputNodeViewModel(addonExportOutputInfoViewModel);

    var fullPathToDirectory = _getFullPathToDirectory.Invoke();
    if (string.IsNullOrEmpty(fullPathToDirectory))
    {
      OutputFullPath = string.Empty;
      return;
    }

    var fullPathToDirectoryInfo = new DirectoryInfo(fullPathToDirectory);

    var addonGameDirectoryFullPath = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dota2AddonName);
    if (!fullPathToDirectoryInfo.FullName.StartsWith(addonGameDirectoryFullPath, StringComparison.InvariantCultureIgnoreCase))
      throw new Exception($"{nameof(RelativePreviewOutputPathViewModel)} - {nameof(UpdateFullPath)} - fullPathToDirectoryInfo.FullName.StartsWith");

    var newPath = fullPathToDirectoryInfo.FullName.Substring(addonGameDirectoryFullPath.Length);

    CommonPartOfPath = addonGameDirectoryFullPath;
    RelativePath = newPath;

    if (newPath[0] == '\\')
      newPath = newPath.Substring(1);

    OutputFullPath = Path.Combine(addonExportOutputInfoViewModel.AddonOutputDirectoryFullPath, newPath);

    var pathInReverseOrder = PathHelper.GetRelativePathDirectories(addonExportOutputInfoViewModel.AddonOutputDirectoryFullPath, OutputFullPath);
    var nodesInReverseOrder = pathInReverseOrder.Select(x => new OutputNodeViewModel(x, null)).ToArray();
    
    nodesInReverseOrder = nodesInReverseOrder.Reverse().ToArray();
    var nextNode = OutputTreeViewModel.Items.Single();
    foreach (var outputNodeViewModel in nodesInReverseOrder)
    {
      outputNodeViewModel.Parent = nextNode; // todo initialization of PArent to constructor

      nextNode.Items.Add(outputNodeViewModel);
      nextNode = outputNodeViewModel;
    }
  }

  #endregion // Public Methods
}