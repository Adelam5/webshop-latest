using Api.Contracts.User;
using Application.Users.Commands.Delete;
using Application.Users.Commands.UpdateDetails;
using Application.Users.Queries.GetById;
using Application.Users.Queries.List;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;
public class UsersController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var query = new ListUsersQuery();
        return HandleResult(await Mediator.Send(query, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        return HandleResult(await Mediator.Send(query, cancellationToken));
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var query = new GetUserByIdQuery(userId);
        return HandleResult(await Mediator.Send(query, cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateDetails(string id, UpdateUserDetailsRequest user, CancellationToken cancellationToken)
    {
        user.Id = id;
        var command = Mapper.Map<UpdateUserDetailsCommand>(user);
        return HandleResult(await Mediator.Send(command, cancellationToken));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id, CancellationToken cancellationToken)
    {
        var query = new DeleteUserCommand(id);
        return HandleResult(await Mediator.Send(query, cancellationToken));
    }

}
