using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Common.WPF;

namespace SBnDota2ModExporter;

public class OutputNodeViewModel : BaseViewModel
{
  public enum enOutputNodeType
  {
    File,
    Directory,
  }

  #region Fields

  private string _name;
  private bool _isExpanded = true;
  private string _fullPath;
  private enOutputNodeType _outputNodeType;
  private FontWeight _fontWeight = FontWeights.Normal;

  private bool _isVirtual;
  private bool _canDeleteNode;

  #endregion // Fields

  #region Ctor

  public OutputNodeViewModel(DirectoryInfo directoryInfo, OutputNodeViewModel? parent)
  {
    Parent = parent;

    _name = directoryInfo.Name;
    _fullPath = directoryInfo.FullName;
    _outputNodeType = enOutputNodeType.Directory;
  }

  public OutputNodeViewModel(FileInfo fileInfo, OutputNodeViewModel? parent)
  {
    Parent = parent;

    _name = fileInfo.Name;
    _fullPath = fileInfo.FullName;
    _outputNodeType = enOutputNodeType.File;
  }

  #endregion // Ctor

  #region Properties

  public OutputNodeViewModel? Parent { get; set; }

  public ObservableCollection<OutputNodeViewModel> Items { get; } = new();

  public string Name
  {
    get => _name;
    set
    {
      _name = value;
      OnPropertyChanged();
    }
  }
  
  public bool IsExpanded
  {
    get => _isExpanded;
    set
    {
      _isExpanded = value;
      OnPropertyChanged();
    }
  }

  public string FullPath
  {
    get => _fullPath;
    set
    {
      _fullPath = value;
      OnPropertyChanged();
    }
  }

  public enOutputNodeType OutputNodeType
  {
    get => _outputNodeType;
    set
    {
      _outputNodeType = value;
      OnPropertyChanged();
    }
  }

  public FontWeight FontWeight
  {
    get => _fontWeight;
    set
    {
      _fontWeight = value;
      OnPropertyChanged();
    }
  }

  public bool IsVirtual
  {
    get => _isVirtual;
    set
    {
      _isVirtual = value;
      OnPropertyChanged();
    }
  }

  public bool CanDeleteNode
  {
    get => _canDeleteNode;
    set
    {
      _canDeleteNode = value;
      OnPropertyChanged();
    }
  }

  #endregion // Properties
}