using System.Net;

namespace Domain.Exceptions.Photo;
public class InvalidProductDescriptionException : AppException
{
    public InvalidProductDescriptionException()
        : base("Product description must not be empty.", HttpStatusCode.BadRequest)
    {
    }
}
