using ValveResourceFormat.Serialization.KeyValues;

namespace ReplaceMissingFilesInsideVsndevts;

public class ReplaceMissingFilesResult
{
  public ReplaceMissingFilesResult(KV3File? kv3File, int countFilesReplaced)
  {
    Kv3File = kv3File;
    CountFilesReplaced = countFilesReplaced;
  }

  public KV3File? Kv3File { get; }
  public int CountFilesReplaced { get; }
}