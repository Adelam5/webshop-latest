
using Application.DeliveryMethods.Queries.ListDeliveryMethods;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class DeliveryMethodsController : ApiController
{
    [HttpGet]
    public async Task<ActionResult> List()
    {
        return HandleResult(await Mediator.Send(new ListDeliveryMethodsQuery()));
    }
}
