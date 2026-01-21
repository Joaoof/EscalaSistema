namespace EscalaSistema.API.Domain.Errors;

public class DomainException : Exception
{
    public DomainError Error { get; }

    public DomainException(DomainError error) : base(error.Title)
    {
        Error = error;
    }
}
