namespace Domain.Core.Cart.Entities;
public class CartItem
{

#pragma warning disable CS8618
    private CartItem() { }
#pragma warning restore CS8618

    public CartItem(string id, string name, decimal price)
    {
        Id = id;
        Quantity = 1;
        Name = name;
        Price = price;
    }

    public CartItem(string id, int quantity, string name, decimal price)
    {
        Id = id;
        Quantity = quantity;
        Name = name;
        Price = price;
    }

    public string Id { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

}
