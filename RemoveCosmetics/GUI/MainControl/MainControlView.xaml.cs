using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.Messaging;
using RemoveCosmetics.GUI.MainControl.HeroViewModels;

namespace RemoveCosmetics.GUI.MainControl;

public partial class MainControlView
{
  public MainControlView()
  {
    InitializeComponent();

    WeakReferenceMessenger.Default.Register<ShowMessageBoxDialogMessage>(this, ShowMessageBoxDialogMessageHandler);
    WeakReferenceMessenger.Default.Register<ConsoleSetTextMessage>(this, ConsoleSetTextMessageHandler);
    WeakReferenceMessenger.Default.Register<ConsoleAppendLineTextMessage>(this, ConsoleAppendTextMessageHandler);
  }

  private void ShowMessageBoxDialogMessageHandler(object recipient, ShowMessageBoxDialogMessage message)
  {
    var messageBoxResult = MessageBox.Show(Window.GetWindow(this), message.MessageBoxText, message.Caption, message.Button, message.Icon);
    message.Callback?.Invoke(messageBoxResult);
  }

  private void ConsoleSetTextMessageHandler(object recipient, ConsoleSetTextMessage message)
  {
    rtbConsole.Document.Blocks.Clear();

    rtbConsole.Document.Blocks.Add(new Paragraph(new Run($"{message.Text}"))
    {
      Margin = new Thickness(0),
      Foreground = message.ForegroundColor,
      FontWeight = message.FontWeight,
    });

    rtbConsole.ScrollToEnd();
  }

  private void ConsoleAppendTextMessageHandler(object recipient, ConsoleAppendLineTextMessage message)
  {
    rtbConsole.Document.Blocks.Add(new Paragraph(new Run($"{message.Text}"))
    {
      Margin = new Thickness(0),
      Foreground = message.ForegroundColor,
      FontWeight = message.FontWeight,
    });

    rtbConsole.ScrollToEnd();
  }

  private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
  {
    var uri = e.Uri;
    var ps = new ProcessStartInfo(uri.ToString())
    {
      UseShellExecute = true,
      Verb = "open"
    };

    Process.Start(ps);
  }

  private void DataGridRight_OnPreviewKeyDown(object sender, KeyEventArgs e)
  {
    try
    {
      var dataGrid = sender as DataGrid;
      if (dataGrid == null)
        return;

      if (e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Escape)
        return; // Ignore navigation keys

      if (e.Key >= Key.A && e.Key <= Key.Z) // Check if it's a letter key
      {
        char pressedChar = e.Key.ToString()[0]; // Get character representation

        var items = dataGrid.Items.Cast<HeroItemViewModel>().ToList();
        int index = items.FindIndex(item => item.HeroName.StartsWith(pressedChar.ToString(), StringComparison.OrdinalIgnoreCase));

        if (index < 0) 
          return;
        
        var itemToSelect = dataGrid.Items[index];
        if (itemToSelect == null)
          return;
          
        if (dataGrid.SelectedIndex >= 0)
        {
          if (dataGrid.Items[dataGrid.SelectedIndex] is HeroItemViewModel selectedItemVm)
          {
            if (selectedItemVm.HeroName.StartsWith(pressedChar.ToString(), StringComparison.OrdinalIgnoreCase))
            {
              if (dataGrid.SelectedIndex + 1 < dataGrid.Items.Count)
              {
                // get next item and it it also starts with same character, select it
                var nextItemIndex = dataGrid.SelectedIndex + 1;
                var nextItem = dataGrid.Items[dataGrid.SelectedIndex + 1];
                if (nextItem is HeroItemViewModel nextItemVm)
                {
                  if (nextItemVm.HeroName.StartsWith(pressedChar.ToString(), StringComparison.OrdinalIgnoreCase))
                  {
                    index = nextItemIndex;
                    itemToSelect = nextItemVm;
                  }
                }
              }
            }
          }
        }

        dataGrid.SelectedIndex = index;
        dataGrid.ScrollIntoView(itemToSelect);
      }
    }
    catch (Exception ex)
    {
      App.Logger.Error(ex.ToString());
    }
  }
}