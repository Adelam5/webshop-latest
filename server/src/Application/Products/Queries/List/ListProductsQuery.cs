using Application.Common.Interfaces.Messaging;

namespace Application.Products.Queries.List;
public sealed record ListProductsQuery() : IQuery<List<ListProductsResponse>>;
