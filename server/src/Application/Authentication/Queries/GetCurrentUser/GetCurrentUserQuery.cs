using Application.Common.Interfaces.Messaging;

namespace Application.Authentication.Queries.GetCurrentUser;
public sealed record GetCurrentUserQuery : IQuery<GetCurrentUserResponse>;
