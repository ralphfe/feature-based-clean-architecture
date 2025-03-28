namespace TodoApp.Application.Features;

public class Result<T>
{
    private readonly Exception? error;
    
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Exception Error => this.error ?? throw new InvalidOperationException("Result is not an error");

    private Result(bool isSuccess, T? value, Exception? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        this.error = error;
    }

    public static Result<T> Success(T value) => new(true, value, null);

    public static Result<T> Failure(Exception error) => new(false, default, error);
    
    public static implicit operator Result<T>(T value) => Success(value);

    public static implicit operator Result<T>(Exception error) => Failure(error);
}