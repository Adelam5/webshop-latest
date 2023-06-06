using System.Net;

namespace Domain.Exceptions;

public class UnauthenticatedException : AppException
{
    public UnauthenticatedException() : base("User is not logged in", HttpStatusCode.Unauthorized)
    {
    }
}
