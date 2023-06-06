using Domain.Constants;
using System.Net;

namespace Domain.Exceptions.Address;

public class InvalidStreetException : AppException
{
    public InvalidStreetException()
        : base($"Street must not be empty and its length should be between {CommonConstants.StringMinLength} and {CommonConstants.StringMaxLength} characters.", HttpStatusCode.BadRequest)
    {
    }
}
