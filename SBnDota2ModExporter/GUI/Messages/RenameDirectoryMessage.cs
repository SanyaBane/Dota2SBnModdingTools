namespace SBnDota2ModExporter.GUI.Messages;

public class RenameDirectoryMessage
{
  public RenameDirectoryMessage(OutputNodeViewModel selectedNode, OutputNodeViewModel[] otherNodesOfSameParent)
  {
    SelectedNode = selectedNode;
    OtherNodesOfSameParent = otherNodesOfSameParent;
  }

  public OutputNodeViewModel SelectedNode { get; }
  public OutputNodeViewModel[] OtherNodesOfSameParent { get; }
}

public class SuccessRenameDirectoryMessage
{
  public SuccessRenameDirectoryMessage(OutputNodeViewModel selectedNode, string directoryNewName)
  {
    SelectedNode = selectedNode;
    DirectoryNewName = directoryNewName;
  }

  public OutputNodeViewModel SelectedNode { get; }
  public string DirectoryNewName { get; }
}