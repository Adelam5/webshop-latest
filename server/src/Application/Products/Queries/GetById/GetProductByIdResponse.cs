namespace Application.Products.Queries.GetById;
public sealed record GetProductByIdResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string PhotoName,
    string PhotoUrl);



