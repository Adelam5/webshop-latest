using Domain.Primitives;

namespace Domain.Core.Customers.Events;
public sealed record CustomerCreatedDomainEvent(Guid Id, string UserId) : DomainEvent(Id);
