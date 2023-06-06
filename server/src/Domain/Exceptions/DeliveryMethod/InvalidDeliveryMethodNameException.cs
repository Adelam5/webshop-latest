using System.Net;

namespace Domain.Exceptions.DeliveryMethod;
public class InvalidDeliveryMethodNameException : AppException
{
    public InvalidDeliveryMethodNameException()
        : base("DeliveryMethod name must not be empty.", HttpStatusCode.BadRequest)
    {
    }
}
