using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common.WPF;

public abstract class BaseViewModel : INotifyPropertyChanged
{
  #region INotifyPropertyChanged

  public event PropertyChangedEventHandler PropertyChanged;

  protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  #endregion
}