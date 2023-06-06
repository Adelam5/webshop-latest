using Domain.Primitives;

namespace Domain.Core.Orders.ValueObjects;
public sealed class OrderDeliveryMethod : ValueObject
{
    private OrderDeliveryMethod(Guid id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public static OrderDeliveryMethod Create(Guid id, string name, decimal price)
    {
        return new OrderDeliveryMethod(id, name, price);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Price;
    }
}
