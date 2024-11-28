using System.Collections.ObjectModel;
using Common.WPF;

namespace SBnDota2ModExporter;

public class OutputTreeViewModel : BaseViewModel
{
  #region Ctor

  public OutputTreeViewModel()
  {
  }

  #endregion // Ctor

  #region Properties

  public ObservableCollection<OutputNodeViewModel> Items { get; } = new();

  #endregion // Properties
}