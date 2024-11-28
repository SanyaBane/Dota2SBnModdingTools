using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Messaging;

namespace Common.WPF;

public abstract class BaseViewModel : INotifyPropertyChanged
{
  #region Fields

  private string? _token;

  #endregion // Fields

  #region Properties

  public string? Token
  {
    protected get { return _token; }
    set
    {
      if (_token == value)
        return;

      _token = value;

      OnTokenChanged();
    }
  }

  #endregion // Properties

  #region Public Methods

  public virtual void RefreshCommands()
  {
  }

  #endregion // Public Methods

  #region Protected Methods

  protected virtual void OnTokenChanged()
  {
    WeakReferenceMessenger.Default.UnregisterAll(this);
  }

  #endregion // Protected Methods

  #region INotifyPropertyChanged

  public event PropertyChangedEventHandler PropertyChanged;

  protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  #endregion
}