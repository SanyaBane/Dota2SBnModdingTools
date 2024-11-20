using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using SBnDota2ModExporter.GUI.ViewModels;

namespace SBnDota2ModExporter.GUI.Views;

public partial class ExportProgressWindow : Window
{
  public static readonly DependencyProperty IsExportingProperty = DependencyProperty.Register(nameof(IsExporting), typeof(bool), typeof(ExportProgressWindow), new PropertyMetadata(false));

  public bool IsExporting
  {
    get => (bool)GetValue(IsExportingProperty);
    set => SetValue(IsExportingProperty, value);
  }

  public ExportProgressWindow()
  {
    InitializeComponent();

    Loaded += OnLoaded;
  }

  public AddonExporterInfoViewModel[] AddonsToExport { get; set; }

  private async void OnLoaded(object sender, RoutedEventArgs e)
  {
    IsExporting = true;

    try
    {
      await StartExportAsync();
    }
    finally
    {
      IsExporting = false;
    }
  }

  private async Task StartExportAsync()
  {
    var progress = new Progress<AddonExportProgress>(message =>
    {
      rtbProgress.Document.Blocks.Add(new Paragraph(new Run(message.Text))
      {
        Foreground = message.ForegroundColor
      });
      
      rtbProgress.ScrollToEnd();
    });
    
    rtbProgress.Document.Blocks.Clear();

    foreach (var loadedAddonExporterInfoViewModel in AddonsToExport)
    {
      rtbProgress.Document.Blocks.Add(new Paragraph(new Run(
        $"===== Exporting addon '{loadedAddonExporterInfoViewModel.Dota2AddonName}' ====="))
      {
        Foreground = Brushes.DarkBlue,
        FontWeight = FontWeights.Bold
      });
      
      rtbProgress.ScrollToEnd();
      
      await loadedAddonExporterInfoViewModel.ExportAddonAsync(progress);
    }
  }

  private void BtnClose_OnClick(object sender, RoutedEventArgs e)
  {
    Close();
  }
}