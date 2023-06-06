using Domain.Exceptions;
using System.Net;

namespace Domain.S3.Exceptions;
public sealed class S3FileDeletionException : AppException
{
    public S3FileDeletionException(string message)
        : base($"Failed to delete file from Amazon S3: {message}", HttpStatusCode.InternalServerError)
    {
    }

}
