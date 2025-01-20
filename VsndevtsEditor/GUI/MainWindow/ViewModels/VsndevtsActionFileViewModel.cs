using Common.WPF;

namespace VsndevtsEditor.GUI.MainWindow.ViewModels;

public class VsndevtsActionFileViewModel : BaseViewModel
{
  #region Fields

  private string _pathToFile;

  #endregion // Fields

  #region Ctor

  public VsndevtsActionFileViewModel(string pathToFile)
  {
    PathToFile = pathToFile;
  }

  #endregion // Ctor

  #region Properties

  public string PathToFile
  {
    get => _pathToFile;
    set
    {
      _pathToFile = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties
}