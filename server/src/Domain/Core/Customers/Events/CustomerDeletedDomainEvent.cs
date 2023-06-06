using Domain.Primitives;

namespace Domain.Core.Customers.Events;
public sealed record CustomerDeletedDomainEvent(Guid Id, string UserId) : DomainEvent(Id);
