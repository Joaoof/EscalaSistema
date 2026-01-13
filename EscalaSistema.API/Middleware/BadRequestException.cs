namespace EscalaSistema.API.Middleware;

public class BadRequestException: Exception
{
    public int StatusCode { get; }
    public BadRequestException(string message, int statusCode) : base(message) 
    {
        StatusCode = 400;
    }

}
