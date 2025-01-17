using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace CommonLib.Helpers;

public static class PathHelper
{
  public static Result ValidateDirectoryNameRecursive(DirectoryInfo directoryInfo)
  {
    ArgumentNullException.ThrowIfNull(directoryInfo);

    if (directoryInfo.Parent == null)
      return Result.Success();

    var invalidFileNameChars = string.Join("", Path.GetInvalidFileNameChars());
    var regex = new Regex("[" + Regex.Escape(invalidFileNameChars) + "]");
    var matches = regex.Matches(directoryInfo.Name);
    if (matches.Count > 0)
    {
      var tmp = string.Join("", matches);
      return Result.Failure($"Path to directory contains not allowed symbols: {tmp}");
    }

    return ValidateDirectoryNameRecursive(directoryInfo.Parent);
  }
}