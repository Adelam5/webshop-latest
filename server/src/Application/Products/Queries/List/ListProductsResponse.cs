namespace Application.Products.Queries.List;
public sealed record ListProductsResponse(Guid Id, string Name, string Description, decimal Price, string Photo_Url);

