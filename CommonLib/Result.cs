namespace CommonLib;

public class Result
{
  public Result(bool success)
  {
    Success = success;
  }

  public Result(string? errorMessage) : this(false)
  {
    ErrorMessage = errorMessage;
  }

  public Result(string? errorMessage, Exception ex) : this(errorMessage)
  {
    Exception = ex;
  }

  public bool Success { get; }
  public bool Failure => !Success;

  public string? ErrorMessage { get; }
  public Exception? Exception { get; init; }
}

public class Result<T> : Result
{
  public Result(bool success) : base(success)
  {
  }

  public Result(string? errorMessage) : base(errorMessage)
  {
  }

  public Result(string? errorMessage, Exception ex) : base(errorMessage, ex)
  {
  }

  public Result(bool success, T? value) : base(success)
  {
    Value = value;
  }

  public T? Value { get; }
}