namespace RemoveCosmetics.GUI.MainControl.PlaceholderCreation;

public class UnpackedVpkCreatorResources
{
  private readonly List<string> _fullPathToFiles = [];

  public required string FullPathToDirectory { get; init; }

  public string[] GetFullPathToFiles() => _fullPathToFiles.ToArray();

  public void AddFullPathToFile(string fullPathToFile)
  {
    _fullPathToFiles.Add(fullPathToFile);
  }
}