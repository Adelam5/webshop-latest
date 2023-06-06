using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Primitives.Result;

namespace Application.Authentication.Commands.VerifyEmail;
internal sealed class VerifyEmailCommandHandler : ICommandHandler<VerifyEmailCommand, bool>
{
    private readonly IAuthService authService;

    public VerifyEmailCommandHandler(IAuthService authService)
    {
        this.authService = authService;
    }

    public async Task<Result<bool>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        return await authService.VerifyEmail(request.Token, request.UserId);
    }
}
