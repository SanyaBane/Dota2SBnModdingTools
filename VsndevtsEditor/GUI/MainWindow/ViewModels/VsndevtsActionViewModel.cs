using Common.WPF;
using VsndevtsEditor.Models;

namespace VsndevtsEditor.GUI.MainWindow.ViewModels;

public class VsndevtsActionViewModel : BaseViewModel
{
  #region Fields

  private VsndevtsActionFileViewModel? _selectedActionFileVm;

  #endregion // Fields

  #region Ctor

  public VsndevtsActionViewModel(string actionName, IReadOnlyCollection<VsndevtsActionFile> actionFiles)
  {
    ActionName = actionName;

    ActionFileVms = new List<VsndevtsActionFileViewModel>();
    foreach (var vsndevtsActionFile in actionFiles)
    {
      ActionFileVms.Add(new VsndevtsActionFileViewModel(vsndevtsActionFile.PathToFile));
    }
  }

  #endregion // Ctor

  #region Properties

  public string ActionName { get; }

  public List<VsndevtsActionFileViewModel> ActionFileVms { get; }

  public VsndevtsActionFileViewModel? SelectedActionFileVm
  {
    get => _selectedActionFileVm;
    set
    {
      if (_selectedActionFileVm == value)
        return;
      
      _selectedActionFileVm = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties
}