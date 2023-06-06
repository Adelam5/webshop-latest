using Application.Common.Interfaces.Messaging;
using Domain.Abstractions.Interfaces.Repositories;
using Domain.Primitives.Result;

namespace Application.Users.Queries.List;
internal sealed class ListUsersQueryHandler : IQueryHandler<ListUsersQuery, List<ListUsersResponse>>
{
    private readonly IUserRepository userRepository;

    public ListUsersQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Result<List<ListUsersResponse>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAll(cancellationToken);

        return users;
    }
}
