using Application.Orders.Commands.Create;
using Application.Orders.Queries.GetById;
using Application.Orders.Queries.GetCurrentUserOrders;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class OrdersController : ApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        return HandleResult(await Mediator.Send(new GetOrderByIdQuery(id), cancellationToken));
    }

    [HttpGet("me")]
    public async Task<ActionResult> GetCurrentUserOrders(CancellationToken cancellationToken)
    {
        return HandleResult(await Mediator.Send(new GetCurrentUserOrdersQuery(), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> Create(CancellationToken cancellationToken)
    {
        var command = new CreateOrUpdateOrderCommand();

        return HandleResult(await Mediator.Send(command, cancellationToken));
    }
}
