using Common.WPF;
using SBnDota2ModExporter.Configs;
using SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.PreviewOutput;

namespace SBnDota2ModExporter.GUI.ViewModels.DestinationOfCopy.Data;

public abstract class BaseDestinationOfCopyDataViewModel : BaseViewModel
{
  #region Properties

  public abstract IPreviewOutputPathViewModel PreviewOutputPathViewModel { get; }

  #endregion // Properties

  #region Events

  public event Action? IsDirtyChange;

  #endregion // Events

  #region Protected Methods

  protected void RaiseIsDirtyChange()
  {
    IsDirtyChange?.Invoke();
  }

  #endregion // Protected Methods
  
  #region Public Methods

  public abstract BaseDestinationOfCopyDataViewModel Clone();

  public abstract BaseDestinationOfCopyDataConfig CreateDestinationOfCopyDataConfig();

  public virtual bool IsValidViewModel() => true;
  
  #endregion // Public Methods
}