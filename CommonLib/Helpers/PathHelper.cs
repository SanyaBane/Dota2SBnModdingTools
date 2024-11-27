using System.Text.RegularExpressions;

namespace CommonLib.Helpers;

public static class PathHelper
{
  public static Result ValidateDirectoryNameRecursive(DirectoryInfo directoryInfo)
  {
    ArgumentNullException.ThrowIfNull(directoryInfo);

    if (directoryInfo.Parent == null)
      return new Result(true);

    var invalidFileNameChars = string.Join("", Path.GetInvalidFileNameChars());
    var regex = new Regex("[" + Regex.Escape(invalidFileNameChars) + "]");
    var matches = regex.Matches(directoryInfo.Name);
    if (matches.Count > 0)
    {
      var tmp = string.Join("", matches);
      return new Result($"Path to directory contains not allowed symbols: {tmp}");
    }

    return ValidateDirectoryNameRecursive(directoryInfo.Parent);
  }
}