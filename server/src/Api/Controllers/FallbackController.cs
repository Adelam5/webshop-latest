using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class FallbackController : Controller
{
    private readonly IWebHostEnvironment _env;

    public FallbackController(IWebHostEnvironment env)
    {
        _env = env;
    }

    public IActionResult Index()
    {
        var basePath = _env.WebRootPath;
        if (_env.IsProduction())
        {
            basePath = Path.Combine(_env.ContentRootPath, "src", "Api", "wwwroot");
        }

        var filePath = Path.Combine(basePath, "index.html");
        return PhysicalFile(filePath, "text/HTML");
    }
}
