using Application.Common.Interfaces.Messaging;

namespace Application.Products.Queries.GetById;
public sealed record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResponse>;
