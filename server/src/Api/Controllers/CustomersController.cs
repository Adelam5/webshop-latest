using Api.Contracts.Customer;
using Application.Customers.Commands.Create;
using Application.Customers.Commands.Delete;
using Application.Customers.Queries.GetById;
using Application.Customers.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
public class CustomersController : ApiController
{

    [HttpGet]
    public async Task<ActionResult> List(CancellationToken cancellationToken)
    {
        return HandleResult(await Mediator.Send(new GetCustomersQuery(), cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        return HandleResult(await Mediator.Send(new GetCustomerByIdQuery(id), cancellationToken));
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Create(CreateCustomerRequest newCustomer, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateCustomerCommand>(newCustomer);

        return HandleResult(await Mediator.Send(command, cancellationToken));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(Guid id, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<DeleteCustomerCommand>(id);

        return HandleResult(await Mediator.Send(command, cancellationToken));
    }
}
