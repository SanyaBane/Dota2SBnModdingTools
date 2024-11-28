using System.IO;
using Common.WPF;

namespace SBnDota2ModExporter.GUI.ViewModels;

public class SpecifyDota2AddonViewModel : BaseViewModel
{
  #region Fields

  private DirectoryInfo? _selectedDota2ContentAddon;
  private readonly Action<bool>? _canExecuteOkCommandCallback;

  #endregion // Fields

  #region Ctor

  public SpecifyDota2AddonViewModel(string currentDota2AddonName, DirectoryInfo dota2AddonsContentDirectoryInfo, Action<bool>? canExecuteOkCommandCallback)
  {
    Dota2AddonsContentDirectoryInfo = dota2AddonsContentDirectoryInfo;
    _canExecuteOkCommandCallback = canExecuteOkCommandCallback;

    Dota2ContentAddons = Dota2AddonsContentDirectoryInfo.GetDirectories();
    if (!string.IsNullOrEmpty(currentDota2AddonName))
    {
      _selectedDota2ContentAddon = Dota2ContentAddons.SingleOrDefault(x => x.Name == currentDota2AddonName);
    }
  }

  #endregion // Ctor

  #region Properties

  public DirectoryInfo Dota2AddonsContentDirectoryInfo { get; }
  public DirectoryInfo[] Dota2ContentAddons { get; }

  public DirectoryInfo? SelectedDota2ContentAddon
  {
    get => _selectedDota2ContentAddon;
    set
    {
      _selectedDota2ContentAddon = value;
      OnPropertyChanged();
      
      _canExecuteOkCommandCallback?.Invoke(_selectedDota2ContentAddon != null);
    }
  }

  #endregion // Properties
}