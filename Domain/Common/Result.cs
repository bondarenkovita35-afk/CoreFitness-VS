namespace Domain.Common;

// Result används för att returnera resultat utan att kasta exception.
public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }

    protected Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    // Skapar ett lyckat resultat.
    public static Result Success()
        => new Result(true, null);

    // Skapar ett misslyckat resultat med felmeddelande.
    public static Result Failure(string error)
        => new Result(false, error);
}

// Generisk version som returnerar data.
public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, T? value, string? error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
        => new Result<T>(true, value, null);

    public static new Result<T> Failure(string error)
        => new Result<T>(false, default, error);
}