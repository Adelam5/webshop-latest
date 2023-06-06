using Api.Contracts.Cart;
using Application.Carts.Commands.AddItem;
using Application.Carts.Commands.CreateOrUpdate;
using Application.Carts.Commands.Delete;
using Application.Carts.Commands.RemoveItem;
using Application.Carts.Commands.UpdateDeliveryMethod;
using Application.Carts.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CartsController : ApiController
{

    [HttpGet]
    public async Task<ActionResult> GetCart()
    {
        return HandleResult(await Mediator.Send(new GetCartQuery()));
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrUpdate(CreateOrUpdateCartRequest request)
    {
        var command = Mapper.Map<CreateOrUpdateCartCommand>(request);

        return HandleResult(await Mediator.Send(command));
    }

    [HttpDelete]
    public async Task<ActionResult> GetById()
    {
        return HandleResult(await Mediator.Send(new DeleteCartCommand()));
    }

    [HttpPut("add-item")]
    public async Task<ActionResult> AddItem(AddItemRequest request)
    {
        var command = Mapper.Map<AddItemCommand>(request);

        return HandleResult(await Mediator.Send(command));
    }

    [HttpPut("remove-item")]
    public async Task<ActionResult> RemoveItem(RemoveItemRequest request)
    {
        var command = Mapper.Map<RemoveItemCommand>(request);

        return HandleResult(await Mediator.Send(command));
    }

    [HttpPut("update-delivery")]
    public async Task<ActionResult> UpdateDeliveryMethod(UpdateDeliveryMethodRequest request)
    {
        var command = Mapper.Map<UpdateDeliveryMethodCommand>(request);

        return HandleResult(await Mediator.Send(command));
    }
}
