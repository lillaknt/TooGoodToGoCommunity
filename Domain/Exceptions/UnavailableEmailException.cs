namespace Domain.Exceptions;

public class UnavailableEmailException : Exception
{
    public UnavailableEmailException(string message) : base(message)
    {
    }
}