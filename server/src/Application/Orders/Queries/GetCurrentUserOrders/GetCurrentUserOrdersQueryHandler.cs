using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Orders.Queries.GetCurrentUserOrders;
internal sealed class GetCurrentUserOrdersQueryHandler : IQueryHandler<GetCurrentUserOrdersQuery, List<GetCurrentUserOrdersResponse>>
{
    private readonly ICurrentUserService currentUserService;
    private readonly IOrderQueriesRepository orderRepository;

    public GetCurrentUserOrdersQueryHandler(ICurrentUserService currentUserService,
        IOrderQueriesRepository orderRepository)
    {
        this.currentUserService = currentUserService;
        this.orderRepository = orderRepository;
    }

    public async Task<Result<List<GetCurrentUserOrdersResponse>>> Handle(GetCurrentUserOrdersQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;

        if (string.IsNullOrEmpty(userId))
            return Result.Failure<List<GetCurrentUserOrdersResponse>>(Errors.Authentication.NotAuthenticated);

        var orders = await orderRepository.GetCurrentUserOrders(userId);

        return Result.Success(orders);
    }
}
