using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using SBnDota2ModExporter.GUI.Messages;
using SBnDota2ModExporter.GUI.ViewModels;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommandsCreateUpdate;

namespace SBnDota2ModExporter.GUI.Views.AddonExportCommands;

public partial class CopyAddonDirectoryCreateUpdateView
{
  private readonly string _token = Guid.NewGuid().ToString();

  public CopyAddonDirectoryCreateUpdateView()
  {
    InitializeComponent();

    DataContextChanged += OnDataContextChanged;

    WeakReferenceMessenger.Default.Register<RenameDirectoryMessage, string>(this, _token, RenameDirectoryMessageHandler);
  }

  private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
  {
    if (e.OldValue is CopyAddonDirectoryCreateUpdateViewModel oldVm)
    {
      oldVm.Token = null;
    }

    if (e.NewValue is CopyAddonDirectoryCreateUpdateViewModel newVm)
    {
      newVm.Token = _token;
    }
  }

  private void RenameDirectoryMessageHandler(object recipient, RenameDirectoryMessage message)
  {
    var owner = Window.GetWindow(this);
    
    var windowCreateUpdateViewModel = new WindowCreateUpdateViewModel();

    var directoryInfo = new DirectoryInfo(message.SelectedNode.FullPath);
    var renameDirectoryViewModel = new RenameDirectoryViewModel(directoryInfo, message.OtherNodesOfSameParent, windowCreateUpdateViewModel.CanExecuteOkCommandCallback);
    var renameDirectoryView = new RenameDirectoryView()
    {
      DataContext = renameDirectoryViewModel
    };
    
    var windowCreateUpdateView = new WindowCreateUpdateView
    {
      DataContext = windowCreateUpdateViewModel,
      WindowCreateUpdateContent = renameDirectoryView,
      Owner = owner,
      // MinWidth = 600,
      // MinHeight = 200,
      WindowStartupLocation = WindowStartupLocation.CenterOwner,
      SizeToContent = SizeToContent.WidthAndHeight,
    };
    
    if (windowCreateUpdateView.ShowDialog() == true)
    {
      WeakReferenceMessenger.Default.Send(new SuccessRenameDirectoryMessage(message.SelectedNode, renameDirectoryViewModel.GetNameResult()), _token);
    }
  }
}