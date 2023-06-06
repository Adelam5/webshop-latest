using Domain.Constants;
using System.Net;

namespace Domain.Exceptions.Address;

public class InvalidCityException : AppException
{
    public InvalidCityException()
        : base($"City must not be empty and its length should be between {CommonConstants.StringMinLength} and {CommonConstants.StringMaxLength} characters.", HttpStatusCode.BadRequest)
    {
    }
}
