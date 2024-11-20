namespace ReplaceMissingFilesInsideVsndevts;

internal class Configuration
{
  public Configuration(string fullPathToDota2ExecutableFile, string replaceValue)
  {
    FullPathToDota2ExecutableFile = fullPathToDota2ExecutableFile;
    ReplaceValue = replaceValue;
  }

  public string FullPathToDota2ExecutableFile { get; }
  public string ReplaceValue { get; }
}