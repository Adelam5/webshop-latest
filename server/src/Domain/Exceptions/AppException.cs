using System.Net;

namespace Domain.Exceptions;

public class AppException : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;
    public AppException() : base() { }

    public AppException(string message) : base(message) { }

    public AppException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}