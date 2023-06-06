using Domain.Core.Common.ValueObjects;
using Domain.Primitives;

namespace Domain.Core.OrderAggregate.Entities;
public sealed class OrderCustomer : Entity
{
#pragma warning disable CS8618
    private OrderCustomer() : base() { }
#pragma warning restore CS8618

    public OrderCustomer(Guid id, string firstName, string lastName,
        string street, string city, string state, string zipcode) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        DeliveryAddress = Address.Create(street, city, state, zipcode);
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Address DeliveryAddress { get; private set; }
}
