using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Primitives.Result;

namespace Application.Authentication.Commands.ResetPassword;
internal sealed class ResetUserPasswordCommandHandler : ICommandHandler<ResetUserPasswordCommand, bool>
{
    private readonly IAuthService authService;

    public ResetUserPasswordCommandHandler(IAuthService authService)
    {
        this.authService = authService;
    }

    public async Task<Result<bool>> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        return await authService.ResetPassword(request);
    }
}
