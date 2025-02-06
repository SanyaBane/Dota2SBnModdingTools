using System.ComponentModel;
using System.Windows.Data;
using Common.WPF;

namespace RemoveCosmetics.GUI.MainControl.HeroViewModels;

public class HeroListsViewModel : BaseViewModel
{
  public HeroListsViewModel()
  {
    HeroMoveToRightCommand = new DelegateCommand(ExecuteHeroMoveToRight);
    HeroMoveToLeftCommand = new DelegateCommand(ExecuteHeroMoveToLeft);

    HeroListLeftViewModel = new HeroListViewModel();
    HeroListLeftViewModel.SelectedItemChange += HeroListLeftViewModel_OnSelectedItemChange;
    HeroListRightViewModel = new HeroListViewModel();
    HeroListRightViewModel.SelectedItemChange += HeroListRightViewModel_OnSelectedItemChange;

    var listHeroesLeftCollectionView = CollectionViewSource.GetDefaultView(HeroListLeftViewModel.Items);
    listHeroesLeftCollectionView.SortDescriptions.Add(new SortDescription(nameof(HeroItemViewModel.DirectoryName), ListSortDirection.Ascending));

    var listHeroesRightCollectionView = CollectionViewSource.GetDefaultView(HeroListRightViewModel.Items);
    listHeroesRightCollectionView.SortDescriptions.Add(new SortDescription(nameof(HeroItemViewModel.DirectoryName), ListSortDirection.Ascending));
  }

  #region Events

  public event Action? ModelStateChange;

  #endregion // Events

  #region Commands

  public DelegateCommand HeroMoveToLeftCommand { get; }
  public DelegateCommand HeroMoveToRightCommand { get; }

  #endregion // Commands

  #region Properties

  public HeroListViewModel HeroListLeftViewModel { get; }
  public HeroListViewModel HeroListRightViewModel { get; }

  #endregion // Properties

  #region Event Handlers

  private void HeroListLeftViewModel_OnSelectedItemChange(HeroItemViewModel? obj)
  {
    HeroListRightViewModel.SilentlyResetSelectedItem();
  }

  private void HeroListRightViewModel_OnSelectedItemChange(HeroItemViewModel? obj)
  {
    HeroListLeftViewModel.SilentlyResetSelectedItem();
  }

  #endregion // Event Handlers

  #region Command Execute Handlers

  private void ExecuteHeroMoveToRight(object obj)
  {
    var leftSelectedItems = HeroListLeftViewModel.SelectedItems.ToArray();
    if (leftSelectedItems.Length == 0)
      return;

    MoveHeroesToRightList(leftSelectedItems);
  }

  private void ExecuteHeroMoveToLeft(object obj)
  {
    var rightSelectedItems = HeroListRightViewModel.SelectedItems.ToArray();
    if (rightSelectedItems.Length == 0)
      return;

    MoveHeroesToLeftList(rightSelectedItems);
  }

  #endregion // Command Execute Handlers

  #region Public Methods

  public void MoveHeroesToRightList(HeroItemViewModel[] leftSelectedItems)
  {
    foreach (var heroItemViewModel in leftSelectedItems)
    {
      HeroListLeftViewModel.Items.Remove(heroItemViewModel);
      HeroListRightViewModel.Items.Add(heroItemViewModel);
    }

    HeroListRightViewModel.SelectedItem = null;
    
    ModelStateChange?.Invoke();
  }

  public void MoveHeroesToLeftList(HeroItemViewModel[] rightSelectedItems)
  {
    foreach (var heroItemViewModel in rightSelectedItems)
    {
      HeroListRightViewModel.Items.Remove(heroItemViewModel);
      HeroListLeftViewModel.Items.Add(heroItemViewModel);
    }

    HeroListLeftViewModel.SelectedItem = null;
    
    ModelStateChange?.Invoke();
  }

  #endregion // Public Methods
}