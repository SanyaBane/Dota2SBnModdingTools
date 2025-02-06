using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.Messaging;

namespace RemoveCosmetics.GUI.MainControl;

public partial class MainControlView : UserControl
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
      Margin = new Thickness(0)
    });
  }

  private void ConsoleAppendTextMessageHandler(object recipient, ConsoleAppendLineTextMessage message)
  {
    rtbConsole.Document.Blocks.Add(new Paragraph(new Run($"{message.Text}"))
    {
      Margin = new Thickness(0)
    });
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
}