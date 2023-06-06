using Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.UnitTests.Middleware;

public class ExceptionHandlerMiddlewareFixture : IDisposable
{
    public HttpClient Client { get; private set; }
    private TestServer _server;

    public ExceptionHandlerMiddlewareFixture()
    {
        ConfigureApp(app => { });
    }

    public void ConfigureApp(Action<IApplicationBuilder> configureApp)
    {
        var builder = new WebHostBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<ILogger<ExceptionHandlerMiddleware>, Logger<ExceptionHandlerMiddleware>>();
            })
            .Configure(app =>
            {
                app.UseMiddleware<ExceptionHandlerMiddleware>();
                configureApp(app);
            });

        _server = new TestServer(builder);
        Client = _server.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
        _server.Dispose();
    }
}