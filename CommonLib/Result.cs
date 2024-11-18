namespace CommonLib;

public class Result<T>
{
  public Result(bool success)
  {
    Success = success;
  }
  
  public Result(bool success, string? errorMessage)
  {
    Success = success;
    ErrorMessage = errorMessage;
  }
  
  public Result(bool success, T? value) : this(success)
  {
    Value = value;
  }
  
  public T? Value { get; }
  
  public bool Success { get; }
  public bool Failure => !Success;
  
  public string? ErrorMessage { get; }
}