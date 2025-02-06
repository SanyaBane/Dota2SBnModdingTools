using System.Collections.ObjectModel;
using Common.WPF;

namespace RemoveCosmetics.GUI.MainControl.HeroViewModels;

public class HeroListViewModel : BaseViewModel
{
  #region Fields

  private HeroItemViewModel? _selectedItem;

  private bool _canRaiseSelectedItemChange = true;

  #endregion // Fields

  #region Ctor

  public HeroListViewModel()
  {
    SelectedItems = new ObservableCollection<HeroItemViewModel>();
  }

  #endregion // Ctor

  #region Events

  public event Action<HeroItemViewModel?>? SelectedItemChange;

  #endregion // Events

  #region Properties

  public ObservableCollection<HeroItemViewModel> Items { get; } = new();

  public ObservableCollection<HeroItemViewModel> SelectedItems { get; set; }

  public HeroItemViewModel? SelectedItem
  {
    get => _selectedItem;
    set
    {
      _selectedItem = value;
      OnPropertyChanged();

      if (_canRaiseSelectedItemChange)
        SelectedItemChange?.Invoke(_selectedItem);
    }
  }

  #endregion // Properties

  #region Public Methods

  public void SilentlyResetSelectedItem()
  {
    var savedCanRaiseSelectedItemChange = _canRaiseSelectedItemChange;
    try
    {
      _canRaiseSelectedItemChange = false;
      SelectedItem = null;
    }
    finally
    {
      _canRaiseSelectedItemChange = savedCanRaiseSelectedItemChange;
    }
  }

  #endregion // Public Methods
}