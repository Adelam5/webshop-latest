using Api.Controllers;
using Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace Api.UnitTests.Controllers;

public class TestApiController : ApiController
{
    public IActionResult PublicHandleResult<T>(Result<T> result)
    {
        return base.HandleResult(result);
    }
}
