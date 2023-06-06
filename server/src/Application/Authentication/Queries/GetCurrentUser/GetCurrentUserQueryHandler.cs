using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Authentication.Queries.GetCurrentUser;
internal sealed class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, GetCurrentUserResponse>
{
    private readonly IAuthService authService;
    private readonly ICurrentUserService currentUserService;

    public GetCurrentUserQueryHandler(IAuthService authService, ICurrentUserService currentUserService)
    {
        this.authService = authService;
        this.currentUserService = currentUserService;
    }

    public async Task<Result<GetCurrentUserResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;

        if (userId is null)
            return Result.Failure<GetCurrentUserResponse>(Errors.Authentication.NotAuthenticated);

        var user = await authService.GetCurrentUser(userId, cancellationToken);

        if (user is null)
            return Result.Failure<GetCurrentUserResponse>(Errors.User.NotFound);

        return user;
    }
}
