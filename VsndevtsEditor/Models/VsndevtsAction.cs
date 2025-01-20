using ValveResourceFormat.Serialization.KeyValues;

namespace VsndevtsEditor.Models;

public class VsndevtsAction
{
  private readonly List<VsndevtsActionFile> _files = new List<VsndevtsActionFile>();

  public IReadOnlyCollection<VsndevtsActionFile> Files => _files;
  
  public required string ActionName { get; init; }
  public required KVValue KValue { get; set; }

  public void AddVsndActionFile(VsndevtsActionFile vsndActionFile)
  {
    _files.Add(vsndActionFile);
  }
}