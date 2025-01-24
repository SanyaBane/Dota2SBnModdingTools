using Common.WPF;
using ValveResourceFormat.Serialization.KeyValues;
using VsndevtsEditor.Models;

namespace VsndevtsEditor.GUI.MainWindow.ViewModels;

public class LoadedVsndevtsFileViewModel : BaseViewModel
{
  public LoadedVsndevtsFileViewModel(string fileFullPath, KV3File parsedKv3File, VsndevtsFile vsndevtsFile)
  {
    FileFullPath = fileFullPath;
    ParsedKv3File = parsedKv3File;
    VsndevtsFile = vsndevtsFile;
  }
  
  public string FileFullPath { get; }
  public KV3File ParsedKv3File { get; }
  public VsndevtsFile VsndevtsFile { get; }
}