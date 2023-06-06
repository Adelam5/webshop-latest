using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Orders.Queries.GetById;

internal sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, GetByIdOrderResponse>
{
    private readonly IOrderQueriesRepository orderRepository;

    public GetOrderByIdQueryHandler(IOrderQueriesRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<Result<GetByIdOrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetById(request.Id);

        if (order is null)
            return Result.Failure<GetByIdOrderResponse>(Errors.Order.NotFound);

        return Result.Success(order);
    }
}
