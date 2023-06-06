using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Primitives.Result;

namespace Application.Authentication.Commands.Login;
internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, string>
{
    private readonly IAuthService authService;

    public LoginUserCommandHandler(IAuthService authService)
    {
        this.authService = authService;
    }

    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await authService.Login(request, cancellationToken);
    }
}
