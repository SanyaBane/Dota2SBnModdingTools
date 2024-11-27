using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SBnDota2ModExporter.GUI.ViewModels;
using SBnDota2ModExporter.GUI.ViewModels.AddonExportCommands;

namespace SBnDota2ModExporter.GUI.Views;

public partial class AddonInfoSavedView
{
  public AddonInfoSavedView()
  {
    InitializeComponent();

    DataContextChanged += OnDataContextChanged;
    
    // DataObject.AddPastingHandler(tbAddonOutputDirectoryName, TextBoxAddonOutputDirectoryName_OnPaste);
  }

  private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
  {
    if (e.OldValue is AddonExporterInfoViewModel oldVm)
    {
      oldVm.ItemStateUpdated -= AddonExporterInfoViewModel_OnItemStateUpdated;
    }

    if (e.NewValue is AddonExporterInfoViewModel newVm)
    {
      newVm.ItemStateUpdated += AddonExporterInfoViewModel_OnItemStateUpdated;
    }
  }

  private void AddonExporterInfoViewModel_OnItemStateUpdated(IAddonExportCommandViewModel vm)
  {
    dataGridAddonExportCommands.ScrollIntoView(vm);
  }

  // private void TextBoxAddonOutputDirectoryName_OnPaste(object sender, DataObjectPastingEventArgs e)
  // {
  //   throw new NotImplementedException();
  // }

  private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
  {
    // TODO validate Ctrl+V, probably using "TextChanged" with attached property to save "oldText", take look here too: https://stackoverflow.com/a/3056168
    
    var tb = (TextBox)sender;

    string newText;
    if (tb.SelectionLength == 0)
    {
      newText = tb.Text.Insert(tb.CaretIndex, e.Text);
    }
    else
    {
      var textStart = tb.Text.Substring(0, tb.CaretIndex);
      var textEnd = tb.Text.Substring(tb.CaretIndex + tb.SelectionLength);
      newText = textStart + e.Text + textEnd;
    }

    if (string.IsNullOrEmpty(newText))
      return;

    // var fullPath = Path.Combine(GlobalManager.Instance.GlobalSettings.OutputDirectoryFullPath, newText);
    //   
    // // https://stackoverflow.com/a/42036026
    // var regex = new Regex("(^([a-z]|[A-Z]):(?=\\\\(?![\\0-\\37<>:\"/\\\\|?*])|\\/(?![\\0-\\37<>:\"/\\\\|?*])|$)|^\\\\(?=[\\\\\\/][^\\0-\\37<>:\"/\\\\|?*]+)|^(?=(\\\\|\\/)$)|^\\.(?=(\\\\|\\/)$)|^\\.\\.(?=(\\\\|\\/)$)|^(?=(\\\\|\\/)[^\\0-\\37<>:\"/\\\\|?*]+)|^\\.(?=(\\\\|\\/)[^\\0-\\37<>:\"/\\\\|?*]+)|^\\.\\.(?=(\\\\|\\/)[^\\0-\\37<>:\"/\\\\|?*]+))((\\\\|\\/)[^\\0-\\37<>:\"/\\\\|?*]+|(\\\\|\\/)$)*()$");
    // if (!regex.IsMatch(fullPath))
    // {
    //   e.Handled = true;
    //   return;
    // }

    // https://stackoverflow.com/a/12689049
    var invalidFileNameChars = string.Join("", Path.GetInvalidFileNameChars());
    Regex containsABadCharacter = new Regex("[" + Regex.Escape(invalidFileNameChars) + "]");
    if (containsABadCharacter.IsMatch(newText))
    {
      e.Handled = true;
    }
  }
}