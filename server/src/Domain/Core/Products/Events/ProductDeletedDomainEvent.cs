using Domain.Primitives;

namespace Domain.Core.Products.Events;

public sealed record ProductDeletedDomainEvent(Guid Id, string PhotoName) : DomainEvent(Id);
