using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using Common.WPF;

namespace SBnDota2ModExporter.GUI.ViewModels;

public class RenameDirectoryViewModel : BaseViewModel, INotifyDataErrorInfo
{
  #region Fields

  private readonly DirectoryInfo _directoryInfo;
  private readonly OutputNodeViewModel[] _otherNodesOfSameParent;
  private readonly Action<bool>? _canExecuteOkCommandCallback;

  private bool _isDirty;
  private string _name;

  #endregion // Fields

  #region Ctor

  public RenameDirectoryViewModel(DirectoryInfo directoryInfo, OutputNodeViewModel[] otherNodesOfSameParent, Action<bool>? canExecuteOkCommandCallback)
  {
    _directoryInfo = directoryInfo;
    _otherNodesOfSameParent = otherNodesOfSameParent;
    _canExecuteOkCommandCallback = canExecuteOkCommandCallback;

    _name = _directoryInfo.Name;
  }

  #endregion // Ctor

  #region Properties

  public bool IsDirty
  {
    get => _isDirty;
    set
    {
      _isDirty = value;
      OnPropertyChanged();
    }
  }

  public string Name
  {
    get => _name;
    set
    {
      _name = value;
      OnPropertyChanged();

      IsDirty = true;

      Validate(ValidateName, nameof(Name));

      _canExecuteOkCommandCallback?.Invoke(IsDirty && !HasErrors);
    }
  }

  #endregion // Properties

  #region Private Methods

  private string ValidateName()
  {
    if (_directoryInfo.Parent == null)
      return "_directoryInfo.Parent == null";

    string newName = GetNameResult();

    if (string.IsNullOrEmpty(newName))
      return "Directory name can not be empty";

    var invalidFileNameChars = string.Join("", Path.GetInvalidFileNameChars());
    Regex containsABadCharacter = new Regex("[" + Regex.Escape(invalidFileNameChars) + "]");
    var matches = containsABadCharacter.Matches(newName);
    if (matches.Count > 0)
    {
      var invalidCharMatch = string.Join("", containsABadCharacter.Matches(newName));
      return $"Directory name contains invalid characters: {invalidCharMatch}";
    }

    var newPath = Path.Combine(_directoryInfo.Parent.FullName, newName);
    if (newPath.Length >= 256)
      return "Full path to directory can not exceed 256 characters.";

    if (_directoryInfo.Exists)
    {
      if (_directoryInfo.Parent.GetDirectories().Any(x => string.Equals(x.Name, newName, StringComparison.InvariantCultureIgnoreCase)))
        return "Directory with same name already exist";
    }

    if (_otherNodesOfSameParent.Any(x => string.Equals(x.Name, newName, StringComparison.InvariantCultureIgnoreCase)))
      return "Directory with same name already exist";

    return string.Empty;
  }

  #endregion // Private Methods

  #region Public Methods

  public string GetNameResult() => Name.Trim();

  #endregion // Public Methods

  #region INotifyDataErrorInfo

  private readonly Dictionary<string, string> _validationErrors = new();

  public IEnumerable GetErrors(string? propertyName)
  {
    if (string.IsNullOrEmpty(propertyName))
      return _validationErrors.Select(x => x.Value);

    if (_validationErrors.TryGetValue(propertyName, out var error))
      return new[] { error };

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