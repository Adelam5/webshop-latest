using System.Net;

namespace Domain.Exceptions.DeliveryMethod;
public class InvalidDeliveryMethodDescriptionException : AppException
{
    public InvalidDeliveryMethodDescriptionException()
        : base("DeliveryMethod description must not be empty.", HttpStatusCode.BadRequest)
    {
    }
}
