using Domain.Constants;
using Domain.Exceptions.DeliveryMethod;
using Domain.Primitives;

namespace Domain.Core.DeliveryMethods;
public sealed class DeliveryMethod : Entity
{
#pragma warning disable CS8618
    private DeliveryMethod() { }
#pragma warning restore CS8618

    public DeliveryMethod(Guid id, string name, string description, string deliveryTime, decimal price) : base(id)
    {
        Name = name;
        Description = description;
        DeliveryTime = deliveryTime;
        Price = price;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public string DeliveryTime { get; private set; }
    public decimal Price { get; private set; }

    public static DeliveryMethod Create(
        string name,
        string description,
        string deliveryTime,
        decimal price)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > CommonConstants.StringMaxLength)
            throw new InvalidDeliveryMethodNameException();

        if (string.IsNullOrWhiteSpace(description) || description.Length > CommonConstants.TextMaxLength)
            throw new InvalidDeliveryMethodDescriptionException();

        if (price < PriceConstants.MinValue)
            throw new InvalidDeliveryMethodPriceException();

        var deliveryMethod = new DeliveryMethod(Guid.NewGuid(), name, description, deliveryTime, price);

        return deliveryMethod;
    }
}
