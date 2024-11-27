using System.Collections;
using System.ComponentModel;
using System.IO;
using Common.WPF;
using CommonLib.Helpers;

namespace SBnDota2ModExporter.GUI.ViewModels;

public class PathToDirectoryViewModel : BaseViewModel, INotifyDataErrorInfo
{
  #region Fields

  private string _fullPath = string.Empty;

  #endregion // Fields

  #region Ctor

  public PathToDirectoryViewModel()
  {
  }

  #endregion // Ctor

  #region Events

  public event Action? FullPathChange;

  #endregion // Events

  #region Properties

  public string FullPath
  {
    get => _fullPath;
    set
    {
      _fullPath = value;
      OnPropertyChanged();

      Validate(ValidateFullPath, nameof(FullPath));

      FullPathChange?.Invoke();
    }
  }

  #endregion // Properties

  #region Private Methods

  private string ValidateFullPath()
  {
    if (string.IsNullOrEmpty(FullPath))
      return "Path to directory can not be empty";

    if (!Path.IsPathRooted(FullPath))
      return "Path to directory is not valid";

    var directoryInfo = new DirectoryInfo(FullPath);

    var resultValidateDirectoryNameRecursive = PathHelper.ValidateDirectoryNameRecursive(directoryInfo);
    if (resultValidateDirectoryNameRecursive.Failure)
      return resultValidateDirectoryNameRecursive.ErrorMessage;

    return string.Empty;
  }

  #endregion // Private Methods

  #region INotifyDataErrorInfo

  private readonly Dictionary<string, string> _validationErrors = new();

  public IEnumerable GetErrors(string? propertyName)
  {
    if (string.IsNullOrEmpty(propertyName))
      return _validationErrors.Select(x => x.Value).AsEnumerable();

    if (_validationErrors.TryGetValue(propertyName, out var error))
      return error.AsEnumerable();

    return Enumerable.Empty<string>();
  }

  private bool _hasErrors;

  public bool HasErrors
  {
    get => _hasErrors;
    private set
    {
      if (_hasErrors == value)
        return;

      _hasErrors = value;
      OnPropertyChanged();
    }
  }

  public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

  private void Validate(Func<string> validation, string propertyName = "")
  {
    var error = validation();
    var isValid = string.IsNullOrWhiteSpace(error);

    if (isValid)
      _validationErrors.Remove(propertyName);
    else
      _validationErrors[propertyName] = error;

    HasErrors = _validationErrors.Count > 0;

    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
  }

  #endregion // INotifyDataErrorInfo
}