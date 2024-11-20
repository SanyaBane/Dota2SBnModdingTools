using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common.WPF;

public abstract class BaseViewModel : INotifyPropertyChanged
{
  #region Public Methods

  public virtual void RefreshCommands()
  {
  }

  #endregion // Public Methods

  #region INotifyPropertyChanged

  public event PropertyChangedEventHandler PropertyChanged;

  protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  #endregion
}