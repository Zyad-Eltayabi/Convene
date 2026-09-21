namespace Application.Core;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Value { get; private set; }
    public string? Error { get; private set; }
    public int StatusCode { get; private set; }

    public static Result<T> Success(T value, int statusCode = 200)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Value = value,
            StatusCode = statusCode
        };
    }

    public static Result<T> Failure(string error, int statusCode = 400)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Error = error,
            StatusCode = statusCode
        };
    }

}