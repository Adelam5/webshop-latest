using System.Net;

namespace Domain.Exceptions.ProductExceptions;
public class InvalidProductNameException : AppException
{
    public InvalidProductNameException()
        : base("Product name must not be empty.", HttpStatusCode.BadRequest)
    {
    }
}
