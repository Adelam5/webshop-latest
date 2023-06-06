using System.Net;

namespace Domain.Exceptions.DeliveryMethod;
public class InvalidDeliveryMethodPriceException : AppException
{
    public InvalidDeliveryMethodPriceException()
        : base("DeliveryMethod price must be greater than or equal to zero.", HttpStatusCode.BadRequest)
    {
    }
}
