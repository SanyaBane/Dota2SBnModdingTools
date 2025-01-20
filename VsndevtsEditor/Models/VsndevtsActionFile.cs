using ValveResourceFormat.Serialization.KeyValues;

namespace VsndevtsEditor.Models;

public class VsndevtsActionFile
{
  /// <summary>
  /// Path to file inside addon.
  /// </summary>
  public required string PathToFile { get; init; }
  
  public required KVValue KVValue { get; init; }
  public required KVObject KVObjectContainer { get; set; }
}