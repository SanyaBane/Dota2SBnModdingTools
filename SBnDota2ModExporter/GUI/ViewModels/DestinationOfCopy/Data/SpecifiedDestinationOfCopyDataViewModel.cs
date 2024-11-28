using System.IO;
using System.Windows;
using Common.WPF;
using CommunityToolkit.Mvvm.Messaging;
using SBnDota2ModExporter.Configs;
using SBnDota2ModExporter.Enums;
using SBnDota2ModExporter.GUI.Messages;
using SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.PreviewOutput;

namespace SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.Data;

public class SpecifiedDestinationOfCopyDataViewModel : BaseDestinationOfCopyDataViewModel
{
  #region Fields

  private readonly Func<string> _getFullPathToDirectory;
  private readonly AddonExportOutputInfoViewModel _addonExportOutputInfoViewModel;
  private readonly string _dota2AddonName;

  private readonly SpecifiedPreviewOutputPathViewModel _specifiedPreviewOutputPathViewModel;

  private OutputNodeViewModel? _selectedNode;
  private DirectoryInfo _addonOutputDirectoryInfo;

  #endregion // Fields

  #region Ctor

  public SpecifiedDestinationOfCopyDataViewModel(Func<string> getFullPathToDirectory, AddonExportOutputInfoViewModel addonExportOutputInfoViewModel, string dota2AddonName)
  {
    _getFullPathToDirectory = getFullPathToDirectory;
    _addonExportOutputInfoViewModel = addonExportOutputInfoViewModel;
    _dota2AddonName = dota2AddonName;
    _specifiedPreviewOutputPathViewModel = new SpecifiedPreviewOutputPathViewModel(getFullPathToDirectory);

    CreateDirectoryCommand = new DelegateCommand(ExecuteCreateDirectory, CanExecuteCreateDirectory);
    RemoveDirectoryCommand = new DelegateCommand(ExecuteRemoveDirectory, CanExecuteRemoveDirectory);
    RenameDirectoryCommand = new DelegateCommand(ExecuteRenameDirectory, CanExecuteRenameDirectory);

    // var addonContentDir = Path.Combine(GlobalManager.Instance.Dota2GameMainInfo.Dota2AddonsGameDirectoryInfo.FullName, dota2AddonName);
    // var addonContentDirInfo = new DirectoryInfo(addonContentDir);

    _addonOutputDirectoryInfo = new DirectoryInfo(addonExportOutputInfoViewModel.AddonOutputDirectoryFullPath);
    var rootNode = new OutputNodeViewModel(_addonOutputDirectoryInfo, null)
    {
      Name = $"{_addonOutputDirectoryInfo.Name} (addon output directory)",
      FontWeight = FontWeights.Bold,
    };

    FillOutputNodeWithDirectories(rootNode, true);

    SpecifiedDestinationNodeViewModel = new OutputTreeViewModel();
    SpecifiedDestinationNodeViewModel.Items.Add(rootNode);
  }

  #endregion // Ctor

  #region Properties

  public OutputTreeViewModel SpecifiedDestinationNodeViewModel { get; }

  public OutputNodeViewModel? SelectedNode
  {
    get => _selectedNode;
    set
    {
      _selectedNode = value;
      OnPropertyChanged();

      RefreshCommands();

      RaiseIsDirtyChange();
    }
  }

  public override IPreviewOutputPathViewModel PreviewOutputPathViewModel => _specifiedPreviewOutputPathViewModel;

  #endregion // Properties

  #region Commands

  public DelegateCommand CreateDirectoryCommand { get; }
  public DelegateCommand RemoveDirectoryCommand { get; }
  public DelegateCommand RenameDirectoryCommand { get; }

  #endregion // Commands

  #region Command Execute Handlers

  private void ExecuteCreateDirectory(object obj)
  {
    if (CanExecuteCreateDirectory(obj) is false)
      return;

    var dirInfo = new DirectoryInfo(Path.Combine(SelectedNode.FullPath, GetNewFolderName(SelectedNode.Items.Select(x => x.Name).ToArray())));
    // if (dirInfo.Exists is false)
    //   dirInfo.Create();

    var newNode = new OutputNodeViewModel(dirInfo, SelectedNode)
    {
      IsVirtual = true
    };

    SelectedNode.Items.Add(newNode);
    SelectedNode.IsExpanded = true;
  }

  private void ExecuteRemoveDirectory(object obj)
  {
    if (CanExecuteRemoveDirectory(obj) is false)
      return;

    var parentNode = SelectedNode.Parent;
    if (parentNode != null)
    {
      parentNode.Items.Remove(SelectedNode);

      SelectedNode = parentNode;
    }
  }

  private void ExecuteRenameDirectory(object obj)
  {
    if (CanExecuteRenameDirectory(obj) is false)
      return;

    var otherNodesOfSameParent = SelectedNode.Parent.Items.Where(x => !ReferenceEquals(SelectedNode, x)).ToArray();
    WeakReferenceMessenger.Default.Send(new RenameDirectoryMessage(SelectedNode, otherNodesOfSameParent), Token);
  }

  #endregion // Command Execute Handlers

  #region Command Can Execute Handlers

  private bool CanExecuteCreateDirectory(object obj)
  {
    return SelectedNode is { OutputNodeType: OutputNodeViewModel.enOutputNodeType.Directory };
  }

  private bool CanExecuteRemoveDirectory(object obj)
  {
    return SelectedNode is { OutputNodeType: OutputNodeViewModel.enOutputNodeType.Directory, IsVirtual: true };
  }

  private bool CanExecuteRenameDirectory(object obj)
  {
    return SelectedNode is { OutputNodeType: OutputNodeViewModel.enOutputNodeType.Directory, IsVirtual: true };
  }

  #endregion // Command Can Execute Handlers

  #region Public Methods

  public override BaseDestinationOfCopyDataViewModel Clone()
  {
    return new SpecifiedDestinationOfCopyDataViewModel(_getFullPathToDirectory, _addonExportOutputInfoViewModel, _dota2AddonName);
  }

  public override BaseDestinationOfCopyDataConfig CreateDestinationOfCopyDataConfig()
  {
    var node = SelectedNode;
    var list = new List<OutputNodeViewModel>();
    while (node != null)
    {
      list.Add(node);
      node = node.Parent;
    }
    
    // var fullPathToSpecifiedDirectory = SelectedNode
    
    return new SpecifiedDirectoryDestinationOfCopyDataConfig()
    {
      FullPathToDirectory = _getFullPathToDirectory.Invoke(),
      SelectedDestinationOfCopyMode = enDestinationOfCopyMode.CopyToSpecifiedDirectory,
      RelativePathToSpecifiedDirectory = "qwe",
    };
  }

  public override bool IsValidViewModel()
  {
    return SelectedNode != null && SelectedNode.OutputNodeType == OutputNodeViewModel.enOutputNodeType.Directory;
  }

  public override void RefreshCommands()
  {
    base.RefreshCommands();

    CreateDirectoryCommand.RaiseCanExecuteChanged();
    RemoveDirectoryCommand.RaiseCanExecuteChanged();
    RenameDirectoryCommand.RaiseCanExecuteChanged();
  }

  #endregion // Public Methods

  #region Protected Methods

  protected override void OnTokenChanged()
  {
    base.OnTokenChanged();

    WeakReferenceMessenger.Default.Register<SuccessRenameDirectoryMessage, string>(this, Token, SuccessRenameDirectoryMessageHandler);
  }

  #endregion // Protected Methods

  #region Private Methods

  private void SuccessRenameDirectoryMessageHandler(object recipient, SuccessRenameDirectoryMessage message)
  {
    if (message.SelectedNode.OutputNodeType != OutputNodeViewModel.enOutputNodeType.Directory)
      throw new Exception();

    message.SelectedNode.FullPath = Path.Combine(message.SelectedNode.Parent.FullPath, message.DirectoryNewName);
    message.SelectedNode.Name = message.DirectoryNewName;
  }

  private static void FillOutputNodeWithDirectories(OutputNodeViewModel node, bool canDeleteNode)
  {
    if (node.OutputNodeType != OutputNodeViewModel.enOutputNodeType.Directory)
      return;

    var dirInfo = new DirectoryInfo(node.FullPath);
    if (dirInfo.Exists is false)
      return;

    var directories = dirInfo.EnumerateDirectories().ToArray();
    foreach (var directoryInfo in directories)
    {
      var newNode = new OutputNodeViewModel(directoryInfo, node)
      {
        CanDeleteNode = canDeleteNode
      };

      FillOutputNodeWithDirectories(newNode, canDeleteNode);

      node.Items.Add(newNode);
    }
  }

  private static string GetNewFolderName(string[] otherDirectoriesNames)
  {
    const string name = "New Folder";

    string current = name;
    int i = 1;

    current = name;
    while (otherDirectoriesNames.Any(x => string.Equals(x, current, StringComparison.InvariantCultureIgnoreCase)))
    {
      current = $"{name} ({++i})";
    }

    return current;
  }

  // private static string GetNewFolderName(string fullPathToParentDictionary)
  // {
  //   const string name = "New Folder";
  //   
  //   string current = name;
  //   int i = 1;
  //   while (Directory.Exists(Path.Combine(fullPathToParentDictionary, current)))
  //   {
  //     current = $"{name}{++i}";
  //   }
  //
  //   return current;
  // }

  #endregion // Private Methods
}