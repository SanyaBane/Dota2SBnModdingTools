using System.Collections.ObjectModel;
using Common.WPF;
using VsndevtsEditor.Configs;
using VsndevtsEditor.Models;

namespace VsndevtsEditor.GUI.MainWindow.ViewModels;

public class VsndevtsActionViewModel : BaseViewModel
{
  #region Fields

  #endregion // Fields

  #region Ctor

  public VsndevtsActionViewModel(VsndevtsAction vsndevtsAction)
  {
    VsndevtsAction = vsndevtsAction;

    ActionName = VsndevtsAction.ActionName;

    foreach (var vsndevtsActionFile in VsndevtsAction.Files)
    {
      var vsndevtsActionFileViewModel = new VsndevtsActionFileViewModel(vsndevtsActionFile.PathToFile);
      AddActionFileVm(vsndevtsActionFileViewModel);
    }
  }

  #endregion // Ctor

  #region Properties

  public bool IsDirty { get; set; }

  public VsndevtsAction VsndevtsAction { get; }

  public string ActionName { get; }

  public ObservableCollection<VsndevtsActionFileViewModel> ActionFileVms { get; } = new();

  public TemplateDirectoryData? TemplateDirectoryData { get; set; }

  #endregion // Properties

  #region Public Methods

  public void AddActionFileVm(VsndevtsActionFileViewModel vsndevtsActionFileViewModel)
  {
    ActionFileVms.Add(vsndevtsActionFileViewModel);
    vsndevtsActionFileViewModel.PathToFileChange += VsndevtsActionFileViewModelOnPathToFileChange;
  }

  public void ClearActionFileVms()
  {
    foreach (var vsndevtsActionFileViewModel in ActionFileVms)
    {
      vsndevtsActionFileViewModel.PathToFileChange -= VsndevtsActionFileViewModelOnPathToFileChange;
    }

    ActionFileVms.Clear();
  }

  #endregion // Public Methods

  #region Private Methods

  private void VsndevtsActionFileViewModelOnPathToFileChange()
  {
    IsDirty = true;
  }

  #endregion // Private Methods
}