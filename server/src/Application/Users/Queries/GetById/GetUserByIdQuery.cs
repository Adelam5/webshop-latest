using Application.Common.Interfaces.Messaging;

namespace Application.Users.Queries.GetById;
public sealed record GetUserByIdQuery(string Id) : IQuery<GetUserByIdResponse>;