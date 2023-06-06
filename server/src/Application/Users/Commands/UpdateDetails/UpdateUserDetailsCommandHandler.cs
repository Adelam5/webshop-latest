using Application.Common.Interfaces.Messaging;
using Domain.Abstractions.Interfaces.Repositories;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Users.Commands.UpdateDetails;

internal sealed class UpdateUserDetailsCommandHandler : ICommandHandler<UpdateUserDetailsCommand, string>
{
    private readonly IUserRepository userRepository;

    public UpdateUserDetailsCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var userId = await userRepository.UpdateDetails(request);

        if (userId == null)
            return Result.Failure<string>(Errors.User.NotFound);

        return userId;
    }
}
