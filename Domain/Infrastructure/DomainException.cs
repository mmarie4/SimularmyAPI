namespace Domain.Infrastructure;

public class DomainException : Exception
{
    public int Code { get; }

    public DomainException(int code, string message)
        : base(message)
    {
        Code = code;
    }
}
