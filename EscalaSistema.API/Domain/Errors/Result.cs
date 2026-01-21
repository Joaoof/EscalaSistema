namespace EscalaSistema.API.Domain.Errors;
public class Result<T>
{
    public bool Success { get; }
    public T? Data { get; }
    public DomainError? Error { get; }

    private Result(T data)
    {
        Success = true;
        Data = data;
    }

    private Result(DomainError error)
    {
        Success = false;
        Error = error;
    }

    public static Result<T> Ok(T data) => new(data);
    public static Result<T> Fail(DomainError error) => new(error);
}

