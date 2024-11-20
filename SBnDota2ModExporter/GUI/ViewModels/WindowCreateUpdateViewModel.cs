using Common.WPF;

namespace SBnDota2ModExporter.GUI.ViewModels;

public class WindowCreateUpdateViewModel : BaseViewModel
{
  #region Fields

  private bool _isEnabledOkButton;
  private string? _title;

  #endregion // Fields

  #region Ctor

  public WindowCreateUpdateViewModel()
  {
    OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);

    CanExecuteOkCommandCallback = (b) =>
    {
      _isEnabledOkButton = b;
      OkCommand.RaiseCanExecuteChanged();
    };
  }

  #endregion // Ctor

  #region Events

  public event Action? OkButtonPress;

  #endregion // Events

  #region Properties

  public Action<bool>? CanExecuteOkCommandCallback { get; set; }

  public string? Title
  {
    get => _title;
    set
    {
      _title = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties

  #region Commands

  public DelegateCommand OkCommand { get; }

  #endregion // Commands

  #region Command Execute Handlers

  private void ExecuteOk(object obj)
  {
    OkButtonPress?.Invoke();
  }

  #endregion // Command Execute Handlers

  #region Command Can Execute Handlers

  private bool CanExecuteOk(object obj)
  {
    return _isEnabledOkButton;
  }

  #endregion // Command Can Execute Handlers
}