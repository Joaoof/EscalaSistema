namespace EscalaSistema.API.Domain.Errors;

public class DomainError
{
    public string Code { get; }
    public string Title { get; }
    public string Detail { get; }
    public int StatusCode { get; }

    public DomainError(string code, string title, string detail, int statusCode)
    {
        Code = code;
        Title = title;
        Detail = detail;
        StatusCode = statusCode;
    }
}
