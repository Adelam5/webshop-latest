using Application.Common.Interfaces.Messaging;
using Domain.Abstractions.Interfaces.Repositories;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Users.Queries.GetById;
internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IUserRepository userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetById(request.Id, cancellationToken);

        if (user is null)
            return Result.Failure<GetUserByIdResponse>(Errors.User.NotFound);

        return user;
    }
}
