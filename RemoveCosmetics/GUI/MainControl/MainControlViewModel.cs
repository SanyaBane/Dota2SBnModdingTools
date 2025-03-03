using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using Common.WPF;
using CommonLib;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
using RemoveCosmetics.Constants;
using RemoveCosmetics.GUI.MainControl.HeroViewModels;
using RemoveCosmetics.GUI.MainControl.PlaceholderCreation;
using RemoveCosmetics.Helpers;
using RemoveCosmetics.Settings;
using SteamDatabase.ValvePak;

namespace RemoveCosmetics.GUI.MainControl;

public class MainControlViewModel : BaseViewModel
{
  #region Fields

  private readonly string[] _ignoreHeroesRegexPatterns =
  [
    "attachto_ghost",
    "aghanim.*",
    "courier",
    "hex",
    "items",
    "pedestal",
    "pedestals",
    "shopkeeper.*",
    "wards",
    "winter_major_effects",
    "world"
  ];

  private string[] _uniqueNames = [];

  private bool _isEnabled;
  private bool _isPlaceholderCreationInProgress;
  private string _dota2ExecutableFullPath = string.Empty;

  private readonly Dota2GameMainInfo _dota2GameMainInfo;
  private readonly HeroIconsProvider _heroIconsProvider;

  private PlaceholderCreationService PlaceholderCreationService { get; } = new();

  #endregion // Fields

  #region Ctor

  public MainControlViewModel(Dota2GameMainInfo dota2GameMainInfo)
  {
    _dota2GameMainInfo = dota2GameMainInfo;

    SetPathToDota2ExeCommand = new DelegateCommand(ExecuteSetPathToDota2Exe);
    StartPlaceholderCreationCommand = new DelegateCommand(ExecuteStartPlaceholderCreation, CanExecuteStartPlaceholderCreation);
    SaveHeroesListCommand = new DelegateCommand(ExecuteSaveHeroesList);
    LoadHeroesListCommand = new DelegateCommand(ExecuteLoadHeroesList);
    ResetHeroesListsCommand = new DelegateCommand(ExecuteResetHeroesLists);

    HeroListsViewModel = new HeroListsViewModel();
    HeroListsViewModel.ModelStateChange += HeroListsViewModelOnModelStateChange;

    Dota2ExecutableFullPath = SettingsManager.Instance.RemoveCosmeticsConfig.Dota2ExeFullPath;

    _heroIconsProvider = new HeroIconsProvider();

    FullPathToExecutableDirectory = SettingsManager.GetFullPathToExecutableDirectory();
  }

  public void InitializeViewModel()
  {
    InitializeHeroesUniqueNames();
    ResetHeroesLists();

    if (SettingsManager.Instance.RemoveCosmeticsConfig.HeroesInRightList.Length > 0)
    {
      LoadSavedHeroes(SettingsManager.Instance.RemoveCosmeticsConfig.HeroesInRightList);
    }

    IsEnabled = true;
  }

  #endregion // Ctor

  #region Commands

  public DelegateCommand SetPathToDota2ExeCommand { get; }
  public DelegateCommand StartPlaceholderCreationCommand { get; }
  public DelegateCommand SaveHeroesListCommand { get; }
  public DelegateCommand ResetHeroesListsCommand { get; }
  public DelegateCommand LoadHeroesListCommand { get; }

  #endregion // Commands

  #region Properties

  public static string Title => $"{ConstantsGeneral.PROGRAM_TITLE} v{VersionHelper.Version.Major}.{VersionHelper.Version.Minor}.{VersionHelper.Version.Build}";

  public string FullPathToExecutableDirectory { get; }

  public HeroListsViewModel HeroListsViewModel { get; }

  public bool IsEnabled
  {
    get => _isEnabled;
    private set
    {
      _isEnabled = value;
      OnPropertyChanged();
    }
  }

  public bool IsPlaceholderCreationInProgress
  {
    get => _isPlaceholderCreationInProgress;
    private set
    {
      _isPlaceholderCreationInProgress = value;
      OnPropertyChanged();
    }
  }

  public string Dota2ExecutableFullPath
  {
    get => _dota2ExecutableFullPath;
    private set
    {
      _dota2ExecutableFullPath = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Event Handlers

  private void HeroListsViewModelOnModelStateChange()
  {
    RefreshCommands();
  }

  #endregion // Event Handlers

  #region Command Execute Handlers

  private void ExecuteSetPathToDota2Exe(object obj)
  {
    var resultCallDialogSetDota2ExePath = SettingsManager.Instance.CallDialogSetDota2ExePath();
    if (resultCallDialogSetDota2ExePath.IsFailure)
    {
      if (!string.IsNullOrEmpty(resultCallDialogSetDota2ExePath.Error))
      {
        WeakReferenceMessenger.Default.Send(new ShowMessageBoxDialogMessage()
        {
          MessageBoxText = resultCallDialogSetDota2ExePath.Error,
          Caption = "Error",
          Button = MessageBoxButton.OK,
          Icon = MessageBoxImage.Error,
        });
      }

      return;
    }

    if (string.IsNullOrEmpty(resultCallDialogSetDota2ExePath.Value))
      return;

    SettingsManager.Instance.RemoveCosmeticsConfig.Dota2ExeFullPath = resultCallDialogSetDota2ExePath.Value;
    Dota2ExecutableFullPath = SettingsManager.Instance.RemoveCosmeticsConfig.Dota2ExeFullPath;

    var updateDota2GameMainInfoResult = SettingsManager.Instance.UpdateDota2GameMainInfo();
    if (updateDota2GameMainInfoResult.IsFailure)
    {
      WeakReferenceMessenger.Default.Send(new ShowMessageBoxDialogMessage()
      {
        MessageBoxText = updateDota2GameMainInfoResult.Error,
        Caption = "Error",
        Button = MessageBoxButton.OK,
        Icon = MessageBoxImage.Error,
      });
    }
  }

  private async void ExecuteStartPlaceholderCreation(object obj)
  {
    if (!CanExecuteStartPlaceholderCreation(obj))
      return;

    var saveFileDialog = new SaveFileDialog()
    {
      Title = "Specify where to create vpk file with placeholder models",
      Filter = $"|*.{ConstantsGeneral.VPK_FILE_EXTENSION}|All files (*.*)|*.*",
      FileName = "pak99_dir.vpk",
    };

    if (!string.IsNullOrEmpty(SettingsManager.Instance.RemoveCosmeticsConfig.PlaceholderVpkFileDirectoryFullPath)
        && Directory.Exists(SettingsManager.Instance.RemoveCosmeticsConfig.PlaceholderVpkFileDirectoryFullPath))
    {
      saveFileDialog.InitialDirectory = SettingsManager.Instance.RemoveCosmeticsConfig.PlaceholderVpkFileDirectoryFullPath;
    }

    if (saveFileDialog.ShowDialog() != true)
      return;

    var saveFileFileInfo = new FileInfo(saveFileDialog.FileName);
    if (saveFileFileInfo.Directory != null)
    {
      if (SettingsManager.Instance.RemoveCosmeticsConfig.PlaceholderVpkFileDirectoryFullPath != saveFileFileInfo.Directory.FullName)
      {
        SettingsManager.Instance.RemoveCosmeticsConfig.PlaceholderVpkFileDirectoryFullPath = saveFileFileInfo.Directory.FullName;
      }
    }

    var savedIsEnabled = IsEnabled;

    try
    {
      IsEnabled = false;
      IsPlaceholderCreationInProgress = true;

      var rightListHeroes = HeroListsViewModel.HeroListRightViewModel.Items.Select(x => x.DirectoryName).ToArray();

      var progress = new Progress<PlaceholderCreationProgress>(message =>
      {
        WeakReferenceMessenger.Default.Send(new ConsoleAppendLineTextMessage()
        {
          Text = message.Text,
          ForegroundColor = message.ForegroundColor,
          FontWeight = message.FontWeight,
          FontSize = message.FontSize,
        });
      });

      WeakReferenceMessenger.Default.Send(new ConsoleSetTextMessage()
      {
        Text = "Start creation of vpk file with placeholder models."
      });

      var createVpkFileWithPlaceholderModelsResult = await PlaceholderCreationService.CreateVpkFileWithPlaceholderModels(progress, rightListHeroes, saveFileDialog.FileName);
      if (createVpkFileWithPlaceholderModelsResult.IsFailure)
      {
        WeakReferenceMessenger.Default.Send(new ConsoleAppendLineTextMessage()
        {
          Text = $"Error: {createVpkFileWithPlaceholderModelsResult.Error}",
          ForegroundColor = Brushes.Red,
        });

        return;
      }

      WeakReferenceMessenger.Default.Send(new ConsoleAppendLineTextMessage()
      {
        Text = $"{Environment.NewLine}Creation of placeholders finished.",
        ForegroundColor = Brushes.Green,
        FontWeight = FontWeights.Bold,
        FontSize = 14
      });
    }
    finally
    {
      IsEnabled = savedIsEnabled;
      IsPlaceholderCreationInProgress = false;
    }
  }

  private void ExecuteSaveHeroesList(object obj)
  {
    if (HeroListsViewModel.HeroListRightViewModel.Items.Count == 0)
    {
      WeakReferenceMessenger.Default.Send(new ConsoleSetTextMessage()
      {
        Text = "Can not save empty list."
      });

      return;
    }

    var list = new List<string>();
    foreach (var heroItemViewModel in HeroListsViewModel.HeroListRightViewModel.Items)
    {
      list.Add(heroItemViewModel.DirectoryName);
    }

    SettingsManager.Instance.RemoveCosmeticsConfig.HeroesInRightList = list.ToArray();

    WeakReferenceMessenger.Default.Send(new ConsoleSetTextMessage()
    {
      Text = "Heroes list (right) saved."
    });
  }

  private void ExecuteLoadHeroesList(object obj)
  {
    var heroDirectoryNames = SettingsManager.Instance.RemoveCosmeticsConfig.HeroesInRightList;
    if (heroDirectoryNames.Length == 0)
    {
      WeakReferenceMessenger.Default.Send(new ConsoleSetTextMessage()
      {
        Text = "Nothing to load."
      });

      return;
    }

    ResetHeroesLists();
    LoadSavedHeroes(heroDirectoryNames);
    RefreshCommands();

    WeakReferenceMessenger.Default.Send(new ConsoleSetTextMessage()
    {
      Text = "Heroes list (right) loaded."
    });
  }

  private void LoadSavedHeroes(string[] heroDirectoryNames)
  {
    var items = HeroListsViewModel.HeroListLeftViewModel.Items.Where(x => heroDirectoryNames.Any(y => y == x.DirectoryName)).ToArray();
    HeroListsViewModel.MoveHeroesToRightList(items);
  }

  private void ExecuteResetHeroesLists(object obj)
  {
    ResetHeroesLists();
    RefreshCommands();

    WeakReferenceMessenger.Default.Send(new ConsoleSetTextMessage()
    {
      Text = "Heroes lists reset complete."
    });
  }

  #endregion // Command Execute Handlers

  #region Command Can Execute Handlers

  private bool CanExecuteStartPlaceholderCreation(object obj)
  {
    if (HeroListsViewModel.HeroListRightViewModel.Items.Count == 0)
      return false;

    return true;
  }

  #endregion // Command Can Execute Handlers

  #region Public Methods

  public override void RefreshCommands()
  {
    base.RefreshCommands();

    StartPlaceholderCreationCommand.RaiseCanExecuteChanged();
  }

  #endregion // Public Methods

  #region Private Methods

  private void InitializeHeroesUniqueNames()
  {
    var package = new Package();
    package.OptimizeEntriesForBinarySearch(StringComparison.OrdinalIgnoreCase);
    package.Read(_dota2GameMainInfo.Pak01DirVpkFileInfo.FullName);

    var vmdlcEntries = package.Entries["vmdl_c"];
    var modelsHeroesEntries = vmdlcEntries
      .Where(x => x.DirectoryName.StartsWith("models/heroes") || x.DirectoryName.StartsWith("models/items"))
      .Select(x => x.DirectoryName);

    var uniqueNames = modelsHeroesEntries
      .Select(path => path.Split('/')) // Split path by '/'
      .Where(parts => parts.Length > 2 && (parts[1] == "heroes" || parts[1] == "items")) // Ensure it's under "models/heroes/" or "models/items/"
      .Select(parts => parts[2]) // Get the directory name after "heroes" or "items"
      .Distinct()
      .ToArray();

    var ignoreRegex = _ignoreHeroesRegexPatterns
      .Select(p => "^" + p + "$")
      .Select(p => new Regex(p, RegexOptions.IgnoreCase))
      .ToList();

    _uniqueNames = uniqueNames
      .Where(dir => !ignoreRegex.Any(regex => regex.IsMatch(dir))) // Ignore matching dirs
      .ToArray();
  }

  private void ResetHeroesLists()
  {
    var savedIsEnabled = IsEnabled;
    try
    {
      IsEnabled = false;

      HeroListsViewModel.HeroListLeftViewModel.Items.Clear();
      HeroListsViewModel.HeroListRightViewModel.Items.Clear();

      foreach (var uniqueName in _uniqueNames)
      {
        var icon = _heroIconsProvider.GetIcon(uniqueName);
        var HeroItemViewModel = new HeroItemViewModel()
        {
          DirectoryName = uniqueName,
          Icon = icon
        };

        HeroListsViewModel.HeroListLeftViewModel.Items.Add(HeroItemViewModel);
      }
    }
    finally
    {
      IsEnabled = savedIsEnabled;
    }
  }

  #endregion // Private Methods
}