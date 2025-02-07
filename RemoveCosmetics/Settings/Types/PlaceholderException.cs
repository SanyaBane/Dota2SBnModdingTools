namespace RemoveCosmetics.Settings.Types;

public class PlaceholderException
{
  public required string Value { get; init; } = string.Empty;
  public required bool IsRegexPattern { get; init; } = false;
}