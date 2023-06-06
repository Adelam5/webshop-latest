using Domain.Core.OrderAggregate.Entities;
using Domain.Core.Orders.Enums;
using Domain.Core.Orders.ValueObjects;
using Domain.Primitives;

namespace Domain.Core.Orders;
public sealed class Order : AggregateRoot, IAuditableEntity
{
    private readonly List<OrderItem> items = new();

#pragma warning disable CS8618
    private Order() : base() { }
#pragma warning restore CS8618

    public Order(Guid id,
    string userId,
    OrderCustomer customer,
    List<OrderItem> items,
    string paymentIntentId,
    OrderDeliveryMethod deliveryMethod,
    decimal subTotal
    ) : base(id)
    {
        UserId = userId;
        Customer = customer;
        PaymentStatus = PaymentStatus.Pending;
        this.items = items;
        PaymentIntentId = paymentIntentId;
        DeliveryMethod = deliveryMethod;
        Subtotal = subTotal;
    }

    public string UserId { get; private set; }
    public OrderCustomer Customer { get; private set; }
    public IReadOnlyList<OrderItem> Items => items.AsReadOnly();
    public PaymentStatus PaymentStatus { get; private set; }
    public decimal Subtotal { get; private set; }
    public OrderDeliveryMethod DeliveryMethod { get; private set; }
    public string PaymentIntentId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime ModifiedOnUtc { get; set; }
    public string? ModifiedBy { get; set; }

    public decimal GetTotal()
    {
        return Subtotal + DeliveryMethod.Price;
    }

    public static Order Create(
        string userId,
        OrderCustomer customer,
        List<OrderItem> items,
        string paymentIntentId,
        OrderDeliveryMethod deliveryMethod)
    {

        decimal subtotal = items.Aggregate(0m, (acc, curr) => acc + curr.Price * curr.Quantity);

        return new Order(
            Guid.NewGuid(),
            userId,
            customer,
            items,
            paymentIntentId,
            deliveryMethod,
            subtotal);
    }

    public Order Update(List<OrderItem> items, OrderDeliveryMethod deliveryMethod)
    {
        this.items.Clear();
        foreach (var item in items)
        {
            this.items.Add(item);
        }

        DeliveryMethod = deliveryMethod;
        Subtotal = items.Aggregate(0m, (acc, curr) => acc + curr.Price * curr.Quantity);

        return this;
    }

    public void PaymentSucceeded()
    {
        PaymentStatus = PaymentStatus.Success;
    }

    public void PaymentFailed()
    {
        PaymentStatus = PaymentStatus.Failure;
    }
}
