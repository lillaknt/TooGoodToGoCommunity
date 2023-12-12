using System;

namespace Domain.Exceptions;

public class InvalidNameLengthException : Exception
{
    public InvalidNameLengthException(string message) : base(message)
    {
    }
}