using Domain.Constants;
using System.Net;

namespace Domain.Exceptions.Address;

public class InvalidStateException : AppException
{
    public InvalidStateException()
        : base($"State must not be empty and its length should be between {CommonConstants.StringMinLength} and {CommonConstants.StringMaxLength} characters.", HttpStatusCode.BadRequest)
    {
    }
}
