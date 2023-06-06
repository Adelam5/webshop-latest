using Application.Common.Interfaces.Messaging;

namespace Application.DeliveryMethods.Queries.ListDeliveryMethods;

public sealed record ListDeliveryMethodsQuery() : IQuery<List<ListDeliveryMethodsResponse>>;