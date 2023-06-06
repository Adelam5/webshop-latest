using Domain.Primitives;

namespace Domain.Core.Cart.ValuesObjects;
public sealed class CartDeliveryMethod : ValueObject
{
    private CartDeliveryMethod(string id, decimal price)
    {
        Id = id ?? "511a434a-52d2-4a28-b5d4-d1be04fdc3f5";
        Price = price;
    }

    public string Id { get; private set; }
    public decimal Price { get; private set; }

    public static CartDeliveryMethod Create(string id, decimal price)
    {
        return new CartDeliveryMethod(id, price);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Price;
    }
}
