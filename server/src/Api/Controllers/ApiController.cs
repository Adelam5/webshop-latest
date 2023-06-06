using Api.Utils;
using AutoMapper;
using Domain.Primitives.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    private ISender mediator = null!;
    private IMapper mapper = null!;

    protected ISender Mediator => mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    protected IMapper Mapper => mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess && result.Value != null)
            return Ok(result.Value);

        if (result.Error.Code.Contains("NotAuthenticated"))
        {
            var unauthorizedResult = base.Unauthorized(ErrorDetailsFactory
                .CreateProblemDetails("Not Authenticated", StatusCodes.Status401Unauthorized, result.Error));
            unauthorizedResult.ContentTypes.Add("application/problem+json");
            return unauthorizedResult;
        }

        var badRequestResult = result switch
        {
            IValidationResult validationResult => base.BadRequest(ErrorDetailsFactory
                    .CreateProblemDetails("Validation Error", StatusCodes.Status400BadRequest, result.Error, validationResult.Errors)),
            _ => base.BadRequest(ErrorDetailsFactory
                    .CreateProblemDetails("Bad Request", StatusCodes.Status400BadRequest, result.Error)),
        };
        badRequestResult.ContentTypes.Add("application/problem+json");
        return badRequestResult;
    }

}
