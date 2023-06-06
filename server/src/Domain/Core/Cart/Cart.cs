using Domain.Constants;
using Domain.Core.Cart.Entities;
using Newtonsoft.Json;

namespace Domain.Core.Cart;
public class Cart
{
    [JsonProperty]
    private readonly List<CartItem> items = new();

#pragma warning disable CS8618
    private Cart() { }
#pragma warning restore CS8618

    public Cart(string userId)
    {
        UserId = userId;
    }

    public Cart(string userId, string productId, string productName, decimal productPrice)
    {
        var cartItem = new CartItem(productId, productName, productPrice);
        items.Add(cartItem);
        UserId = userId;
        Subtotal = productPrice;
    }

    public string UserId { get; private set; }
    public string PaymentIntentId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string DeliveryMethodId { get; private set; } = DeliveryMethodConstants.DefaultDeliveryMethodId;
    public decimal DeliveryPrice { get; private set; } = DeliveryMethodConstants.DefaultDeliveryMethodPrice;
    public decimal Subtotal { get; private set; }
    public IReadOnlyList<CartItem> Items => items.AsReadOnly();

    public Cart AddItem(string productId,
    string name,
    decimal price)
    {
        var existingItem = items.FirstOrDefault(item => item.Id == productId);

        if (existingItem is not null)
        {
            existingItem.Quantity++;
        }
        else
        {
            var newItem = new CartItem(productId, name, price);
            items.Add(newItem);
        }

        Subtotal = items.Sum(item => item.Quantity * item.Price);

        return this;
    }

    public Cart AddItems(string productId, int quantity,
        string name, decimal price)
    {
        var newItem = new CartItem(productId, quantity, name, price);
        items.Add(newItem);

        Subtotal = items.Sum(item => item.Quantity * item.Price);

        return this;
    }

    public Cart RemoveItem(string productId)
    {
        var existingItem = items.FirstOrDefault(item => item.Id == productId);

        if (existingItem is null) return this;

        if (existingItem.Quantity == 1)
        {
            items.Remove(existingItem);
        }
        else
        {
            existingItem.Quantity--;
        }

        Subtotal = items.Sum(item => item.Quantity * item.Price);

        return this;
    }

    public Cart ClearItems()
    {
        items.Clear();

        return this;
    }

    public Cart UpdateDeliveryMethod(string deliveryMethodId, decimal deliveryMethodPrice)
    {
        DeliveryMethodId = deliveryMethodId;
        DeliveryPrice = deliveryMethodPrice;

        return this;
    }

    public void SetPrice(decimal subtotal, decimal deliveryPrice)
    {
        Subtotal = subtotal;
        DeliveryPrice = deliveryPrice;
    }
}
