using System.Net;

namespace Domain.Exceptions.S3;
public sealed class S3FileUploadException : AppException
{
    public S3FileUploadException(string message)
        : base($"Failed to upload file to Amazon S3: {message}", HttpStatusCode.InternalServerError)
    {
    }

}
