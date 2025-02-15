using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Common.WPF;
using VsndevtsEditor.Configs;
using VsndevtsEditor.Models;

namespace VsndevtsEditor.GUI.MainWindow.ViewModels;

public class VsndevtsActionViewModel : BaseViewModel
{
  #region Fields

  private TemplateDirectoryData? _templateDirectoryData;

  #endregion // Fields

  #region Ctor

  public VsndevtsActionViewModel(VsndevtsAction vsndevtsAction)
  {
    VsndevtsAction = vsndevtsAction;

    ActionName = VsndevtsAction.ActionName;

    foreach (var vsndevtsActionFile in VsndevtsAction.Files)
    {
      var vsndevtsActionFileViewModel = new VsndevtsActionFileViewModel(vsndevtsActionFile.PathToFile, false);
      AddActionFileVm(vsndevtsActionFileViewModel);
    }
  }

  #endregion // Ctor

  #region Properties

  public VsndevtsActionViewModel? Self { get; private set; }

  public bool IsDirty { get; set; }

  public VsndevtsAction VsndevtsAction { get; }

  public string ActionName { get; }

  public ObservableCollection<VsndevtsActionFileViewModel> ActionFileVms { get; } = new();

  public TemplateDirectoryData? TemplateDirectoryData
  {
    get => _templateDirectoryData;
    private set
    {
      _templateDirectoryData = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Public Methods

  public void UpdateTemplateDirectoryData()
  {
    foreach (TemplateDirectoryData templateDir in GlobalManager.Instance.TemplateDirectoriesSettings.TemplateDirectories)
    {
      if (Regex.IsMatch(ActionName, @"^.+[_]" + templateDir.ScriptAction + "[_][0-9].+$"))
      {
        TemplateDirectoryData = templateDir;
        break;
      }
    }

    UpdateSelf();
  }

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

  private void UpdateSelf()
  {
    Self = null;
    OnPropertyChanged(nameof(Self));
    Self = this;
    OnPropertyChanged(nameof(Self));
  }

  private void VsndevtsActionFileViewModelOnPathToFileChange()
  {
    IsDirty = true;
  }

  #endregion // Private Methods
}