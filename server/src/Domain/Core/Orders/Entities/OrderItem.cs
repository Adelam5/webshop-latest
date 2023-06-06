using Domain.Primitives;

namespace Domain.Core.OrderAggregate.Entities;
public sealed class OrderItem : Entity
{
#pragma warning disable CS8618
    private OrderItem() : base() { }
#pragma warning restore CS8618

    public OrderItem(Guid id, string name, decimal price, int quantity, string photoUrl = "default-photo.jpg") : base(id)
    {
        Name = name;
        PhotoUrl = photoUrl;
        Price = price;
        Quantity = quantity;
    }

    public string Name { get; private set; }
    public string PhotoUrl { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }


}
