using Application.Common.Interfaces.Messaging;

namespace Application.Users.Queries.List;
public sealed record ListUsersQuery() : IQuery<List<ListUsersResponse>>;
