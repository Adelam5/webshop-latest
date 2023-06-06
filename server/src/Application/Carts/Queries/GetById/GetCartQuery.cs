using Application.Common.Interfaces.Messaging;
using Domain.Core.Cart;

namespace Application.Carts.Queries.GetById;

public record GetCartQuery() : IQuery<Cart>;
