using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Primitives.Result;

namespace Application.Authentication.Commands.Logout;
internal sealed class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand>
{
    private readonly IAuthService authService;

    public LogoutUserCommandHandler(IAuthService authService)
    {
        this.authService = authService;
    }

    public Task<Result> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        authService.Logout();

        return Task.FromResult(Result.Success());
    }
}
