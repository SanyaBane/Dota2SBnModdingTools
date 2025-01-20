using ValveResourceFormat.Serialization.KeyValues;

namespace VsndevtsEditor.Models;

public class VsndevtsFile
{
  private readonly List<VsndevtsAction> _actions = new List<VsndevtsAction>();

  public IReadOnlyCollection<VsndevtsAction> Actions => _actions;
  public required KV3File Kv3File { get; set; }

  public void AddVsndevtsAction(VsndevtsAction vsndActionFile)
  {
    _actions.Add(vsndActionFile);
  }
}