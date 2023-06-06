using System.Net;

namespace Domain.Exceptions;

public sealed class UnauthorizedException : AppException
{
    public UnauthorizedException() : base("Unauthorized access.", HttpStatusCode.Forbidden)
    {
    }
}
