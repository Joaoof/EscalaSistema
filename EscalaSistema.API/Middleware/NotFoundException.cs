namespace EscalaSistema.API.Middleware;

public class NotFoundException: Exception
{
    public int StatusCode { get; }
    public NotFoundException(string message) : base(message)
    {
        StatusCode = 404;
    }
}
