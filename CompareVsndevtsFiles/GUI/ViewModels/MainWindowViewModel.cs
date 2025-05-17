using System.IO;
using Common.WPF;
using CommonLib;
using Microsoft.Win32;

namespace CompareVsndevtsFiles.GUI.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
  #region Fields

  private string _selectedVsndevtsFileFullPath;

  #endregion // Fields

  #region Ctor

  public MainWindowViewModel()
  {
    OpenVsndevtsCommand = new DelegateCommand(ExecuteOpenVsndevts);

    // var entryAssemblyLocation = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location);

    // var builder = new ConfigurationBuilder();
    // builder.SetBasePath(entryAssemblyLocation.Directory.FullName)
    //   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    //
    // IConfigurationRoot configuration = builder.Build();
    //
    //
    // string fullPathToDota2Executable = configuration["PathToDota2ExeFile"];
    // string replaceValue = configuration["ReplaceValue"];

    string fullPathToDota2Exe = "F:\\Games\\SteamLibrary\\steamapps\\common\\dota 2 beta\\game\\bin\\win64\\dota2.exe"; // todo get from config or set by user

    var resultCreateDota2GameMainInfo = Dota2GameMainInfo.CreateDota2GameMainInfo(fullPathToDota2Exe, true);
    if (resultCreateDota2GameMainInfo.IsFailure)
      throw new NotImplementedException();

    Dota2GameMainInfo = resultCreateDota2GameMainInfo.Value;

    // Console.WriteLine($"Dota 2 directory: '{dota2Executable.Directory}'");
  }

  #endregion // Ctor

  #region Properties

  public Dota2GameMainInfo Dota2GameMainInfo { get; }
  public List<string> OriginalGameSoundEvents { get; set; }


  public string SelectedVsndevtsFileFullPath
  {
    get => _selectedVsndevtsFileFullPath;
    set
    {
      _selectedVsndevtsFileFullPath = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Commands

  public DelegateCommand OpenVsndevtsCommand { get; }

  #endregion // Commands


  private void ExecuteOpenVsndevts(object obj)
  {
    var openFileDialog = new OpenFileDialog
    {
      Title = "Select your kv3 voicelines script",
      Filter = $"(*.{ConstantsCommon.VSNDEVTS_FORMAT})|*.{ConstantsCommon.VSNDEVTS_FORMAT}|All files (*.*)|*.*"
    };

    if (openFileDialog.ShowDialog() != true)
      return;

    var selectedVsndevtsFile = new FileInfo(openFileDialog.FileName);
    if (selectedVsndevtsFile.Exists is false)
    {
      throw new NotImplementedException();
    }

    SelectedVsndevtsFileFullPath = selectedVsndevtsFile.FullName;

    var dotaAddonInfo = Dota2AddonInfo.GetDotaAddonInfo(selectedVsndevtsFile, Dota2GameMainInfo);
    if (dotaAddonInfo.IsFailure)
    {
      throw new NotImplementedException();
    }

    var vsndevtsFileNameWithoutExtension = Path.GetFileNameWithoutExtension(selectedVsndevtsFile.FullName);

    var relativePathFromDotaAddon = selectedVsndevtsFile.FullName.Substring(dotaAddonInfo.Value.Dota2GameMainInfo.Dota2AddonsContentDirectoryInfo.FullName.Length);
    var dotaAddonRelativePath = relativePathFromDotaAddon.Substring(1);

    var vsndevtsInsideAddonFilePath = dotaAddonRelativePath.Substring(dotaAddonRelativePath.IndexOf('\\') + 1);
    var vsndevtsInsideAddonDirectoryPath = vsndevtsInsideAddonFilePath.Substring(0, vsndevtsInsideAddonFilePath.LastIndexOf('\\'));

    // var package = new Package();
    // package.OptimizeEntriesForBinarySearch(StringComparison.OrdinalIgnoreCase);
    //
    // package.Read(_dotaGameMainInfo.Pak01DirVpkFile.FullName);
    //
    // var entriesVsndevtsCompiled = package.Entries.Single(x => x.Key == "vsndevts_c");
    //
    // var sameVsndevtsInsidePak01DirFile = entriesVsndevtsCompiled.Value.SingleOrDefault(x => x.FileName == vsndevtsFileNameWithoutExtension && x.DirectoryName == vsndevtsInsideAddonDirectoryPath.Replace('\\', '/'));
    // if (sameVsndevtsInsidePak01DirFile == null)
    // {
    //   return new Result<KV3File?>(false, $"Can not find file '{Path.Combine(vsndevtsInsideAddonDirectoryPath, passedVsndevtsFile.Name)}' inside of Dota2 original files.");
    // }

    // Console.WriteLine($"Program will parse file '{passedVsndevtsFile.Name}' and search for not existing audio files...");

    // openFileDialog.FileName
  }
}