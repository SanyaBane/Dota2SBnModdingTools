namespace RemoveCosmetics.Settings.Types;

public class PlaceholderFileException
{
  public required string Value { get; init; } = string.Empty;
  public required bool IsRegexPattern { get; init; } = false;
  public string? Comment { get; init; }
}