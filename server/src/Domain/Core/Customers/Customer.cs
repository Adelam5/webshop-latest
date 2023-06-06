using Domain.Core.Common.ValueObjects;
using Domain.Core.Customers.Events;
using Domain.Exceptions.User;
using Domain.Primitives;

namespace Domain.Core.Customers;

public sealed class Customer : AggregateRoot, IAuditableEntity
{
    //private readonly List<Guid> orderIds = new();

#pragma warning disable CS8618
    private Customer() : base() { }
#pragma warning restore CS8618

    private Customer(Guid id, string userId, string street, string city, string state, string zipcode) : base(id)
    {
        UserId = userId;
        Address = Address.Create(street, city, state, zipcode);
    }

    public string UserId { get; set; }
    public Address Address { get; set; }
    // public IReadOnlyList<Guid> OrderIds => orderIds.AsReadOnly();
    public DateTime CreatedOnUtc { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime ModifiedOnUtc { get; set; }
    public string? ModifiedBy { get; set; }

    public static Customer Create(string userId, string street, string city, string state, string zipcode)
    {
        if (!Guid.TryParse(userId, out Guid _))
        {
            throw new InvalidUserIdException();
        }

        var customer = new Customer(
            Guid.NewGuid(),
            userId, street, city, state, zipcode);

        customer.RaiseDomainEvent(new CustomerCreatedDomainEvent(
            Guid.NewGuid(),
            customer.UserId));

        return customer;
    }

    public void UpdateAddress(string street, string city, string state, string zipcode)
    {
        Address = Address.Create(street, city, state, zipcode);
    }

    public void MarkAsDeleted()
    {
        RaiseDomainEvent(new CustomerDeletedDomainEvent(Guid.NewGuid(), UserId));
    }
}
