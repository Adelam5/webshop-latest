using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Primitives.Result;

namespace Application.Authentication.Commands.Register;
internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, string>
{
    private readonly IAuthService authService;

    public RegisterUserCommandHandler(IAuthService authService)
    {
        this.authService = authService;
    }

    public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await authService.Register(request, cancellationToken);
    }
}
