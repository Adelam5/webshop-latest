using Application.Common.Interfaces.Messaging;
using Domain.Abstractions.Interfaces.Repositories;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Users.Commands.Delete;
internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, string>
{
    private readonly IUserRepository userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userId = await userRepository.Remove(request.Id);

        if (userId == null)
            return Result.Failure<string>(Errors.User.NotFound);

        return userId;
    }
}
