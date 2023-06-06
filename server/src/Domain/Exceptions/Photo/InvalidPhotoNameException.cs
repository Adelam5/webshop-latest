using System.Net;

namespace Domain.Exceptions.Photo;
public class InvalidPhotoNameException : AppException
{
    public InvalidPhotoNameException()
        : base("Invalid photo name.", HttpStatusCode.BadRequest)
    {
    }
}
