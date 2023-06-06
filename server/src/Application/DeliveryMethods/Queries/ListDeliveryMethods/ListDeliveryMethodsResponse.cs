namespace Application.DeliveryMethods.Queries.ListDeliveryMethods;
public sealed record ListDeliveryMethodsResponse(Guid Id, string Name, string Description, string DeliveryTime, decimal Price);