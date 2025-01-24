using Common.WPF;

namespace VsndevtsEditor.GUI.MainWindow.ViewModels;

public class VsndevtsActionFileViewModel : BaseViewModel
{
  #region Fields

  private string _pathToFile;
  private bool _isDirty;

  #endregion // Fields

  #region Ctor

  public VsndevtsActionFileViewModel(string pathToFile, bool isDirty)
  {
    _pathToFile = pathToFile;
    _isDirty = isDirty;
  }

  #endregion // Ctor

  #region Events

  public event Action? PathToFileChange;

  #endregion // Events

  #region Properties

  public string PathToFile
  {
    get => _pathToFile;
    set
    {
      _pathToFile = value;
      OnPropertyChanged();

      IsDirty = true;
      PathToFileChange?.Invoke();
    }
  }

  public bool IsDirty
  {
    get => _isDirty;
    set
    {
      _isDirty = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties
}