using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Authentication.Commands.ChangePassword;
internal sealed class ChangeUserPasswordCommandHandler : ICommandHandler<ChangeUserPasswordCommand, bool>
{
    private readonly IAuthService authService;
    private readonly ICurrentUserService currentUserService;

    public ChangeUserPasswordCommandHandler(IAuthService authService, ICurrentUserService currentUserService)
    {
        this.authService = authService;
        this.currentUserService = currentUserService;
    }

    public async Task<Result<bool>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;

        if (userId is null)
            return Result.Failure<bool>(Errors.Authentication.NotAuthenticated);

        return await authService.ChangePassword(userId, request);
    }
}
