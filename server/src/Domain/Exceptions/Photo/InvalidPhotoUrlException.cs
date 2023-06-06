using System.Net;

namespace Domain.Exceptions.Photo;
public class InvalidPhotoUrlException : AppException
{
    public InvalidPhotoUrlException()
        : base("Invalid photo URL format.", HttpStatusCode.BadRequest)
    {
    }
}
