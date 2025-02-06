﻿using System.Windows;

namespace RemoveCosmetics.GUI.MainControl;

public class ShowMessageBoxDialogMessage
{
  public required string MessageBoxText { get; init; }
  public required string Caption { get; init; }
  public required MessageBoxButton Button { get; init; }
  public required MessageBoxImage Icon { get; init; }
  public Action<MessageBoxResult>? Callback { get; init; }
}

public class ConsoleSetTextMessage
{
  public required string Text { get; init; }
}

public class ConsoleAppendLineTextMessage
{
  public required string Text { get; init; }
}