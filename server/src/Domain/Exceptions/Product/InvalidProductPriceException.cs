using System.Net;

namespace Domain.Exceptions.ProductExceptions;
public class InvalidProductPriceException : AppException
{
    public InvalidProductPriceException()
        : base("Product price must be greater than or equal to zero.", HttpStatusCode.BadRequest)
    {
    }
}
