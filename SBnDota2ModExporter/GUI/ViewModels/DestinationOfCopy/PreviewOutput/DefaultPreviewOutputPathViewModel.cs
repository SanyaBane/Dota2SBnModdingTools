using System.IO;
using System.Windows;
using Common.WPF;

namespace SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.PreviewOutput;

public class DefaultPreviewOutputPathViewModel : BaseViewModel, IPreviewOutputPathViewModel
{
  #region Fields

  protected readonly Func<string> _getFullPathToDirectory;

  private string _outputFullPath = string.Empty;

  #endregion // Fields

  #region Ctor

  public DefaultPreviewOutputPathViewModel(Func<string> getFullPathToDirectory)
  {
    _getFullPathToDirectory = getFullPathToDirectory;

    OutputTreeViewModel = new OutputTreeViewModel();
  }

  #endregion // Ctor

  #region Properties

  public string OutputFullPath
  {
    get => _outputFullPath;
    protected set
    {
      _outputFullPath = value;
      OnPropertyChanged();
    }
  }
  
  public OutputTreeViewModel OutputTreeViewModel { get; }

  #endregion // Properties

  #region Public Methods

  public virtual void UpdateFullPath(string dota2AddonName, AddonExportOutputInfoViewModel addonExportOutputInfoViewModel)
  {
    ResetPreviewOutputNodeViewModel(addonExportOutputInfoViewModel);
    
    var fullPathToDirectory = _getFullPathToDirectory.Invoke();
    if (string.IsNullOrEmpty(fullPathToDirectory))
    {
      OutputFullPath = string.Empty;
      return;
    }

    var fullPathToDirectoryInfo = new DirectoryInfo(fullPathToDirectory);
    OutputFullPath = Path.Combine(addonExportOutputInfoViewModel.AddonOutputDirectoryFullPath, fullPathToDirectoryInfo.Name);
    
    var addonGameDirectoryFullPath = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dota2AddonName);
    if (fullPathToDirectoryInfo.FullName.StartsWith(addonGameDirectoryFullPath, StringComparison.InvariantCultureIgnoreCase))
    {
      var outputDirInfo = new DirectoryInfo(OutputFullPath);
      
      var rootNode = OutputTreeViewModel.Items.Single();
      rootNode.Items.Add(new OutputNodeViewModel(outputDirInfo, rootNode));
    }
  }

  #endregion // Public Methods

  #region Protected Methods

  protected void ResetPreviewOutputNodeViewModel(AddonExportOutputInfoViewModel addonExportOutputInfoViewModel)
  {
    OutputTreeViewModel.Items.Clear();
    var addonOutputDirectoryInfo = new DirectoryInfo(addonExportOutputInfoViewModel.AddonOutputDirectoryFullPath);

    var rootNode = new OutputNodeViewModel(addonOutputDirectoryInfo, null)
    {
      Name = $"{addonOutputDirectoryInfo.Name} (addon output directory)",
      FontWeight = FontWeights.Bold,
    };
    
    OutputTreeViewModel.Items.Add(rootNode);
  }

  #endregion // Protected Methods
}