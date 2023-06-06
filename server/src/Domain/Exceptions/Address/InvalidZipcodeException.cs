using Domain.Constants;
using System.Net;

namespace Domain.Exceptions.Address;

public class InvalidZipcodeException : AppException
{
    public InvalidZipcodeException()
        : base($"Zipcode must not be empty, should be exactly {AddressConstants.ZipcodeLength} digits", HttpStatusCode.BadRequest)
    {
    }
}
