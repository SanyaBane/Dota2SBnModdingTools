using System.IO;

namespace SBnDota2ModExporter;

// https://stackoverflow.com/a/690980
public static class FileManager
{
  public static void CopyDirectory(DirectoryInfo source, DirectoryInfo target, bool isCopySubdirectories)
  {
    var newDirInfo = new DirectoryInfo(Path.Combine(target.FullName, source.Name));
    
    if (!newDirInfo.Exists)
      newDirInfo.Create();
    
    CopyDirectoryContent(source, newDirInfo, isCopySubdirectories);
  }
  
  public static void CopyDirectoryContent(DirectoryInfo source, DirectoryInfo target, bool isCopySubdirectories)
  {
    if (source.FullName.Equals(target.FullName, StringComparison.CurrentCultureIgnoreCase))
      return;

    if (target.Exists == false)
      Directory.CreateDirectory(target.FullName);

    // Copy each file into it's new directory.
    foreach (FileInfo fi in source.GetFiles())
    {
      // Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
      fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
    }

    if (isCopySubdirectories)
    {
      // Copy each subdirectory using recursion.
      foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
      {
        DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);

        CopyDirectoryContent(diSourceSubDir, nextTargetSubDir, true);
      }
    }
  }
}