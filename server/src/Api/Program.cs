using Api;
using Api.Middleware;
using Api.Utils;
using Application;
using Infrastructure;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var basePath = builder.Environment.ContentRootPath;
if (builder.Environment.IsProduction())
{
    basePath = Path.Combine(basePath, "src", "Api");
}

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(basePath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

if (builder.Environment.IsDevelopment())
{
    configurationBuilder.AddUserSecrets(typeof(Program).Assembly);
}

var configuration = configurationBuilder.Build();

builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(configuration));

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(configuration, builder.Environment);

var app = builder.Build();

var scope = app.Services.CreateScope();
await DataHelper.ManageData(scope.ServiceProvider);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
       Path.Combine(basePath, "wwwroot")),
    RequestPath = "/StaticFiles"
});

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToController("Index", "Fallback");

app.Run();
