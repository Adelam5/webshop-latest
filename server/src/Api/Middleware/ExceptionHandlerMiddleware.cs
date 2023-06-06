using Api.Utils;
using Domain.Exceptions;
using Domain.Primitives.Result;
using System.Net;
using System.Text.Json;

namespace Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlerMiddleware> logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var response = context.Response;

        try
        {
            await next(context);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            var statusCode = HttpStatusCode.InternalServerError;

            if (ex is AppException exception)
                statusCode = exception.StatusCode;

            await HandleException(context, ex, statusCode);

        }
    }

    private async Task HandleException(HttpContext context, Exception ex, HttpStatusCode statusCode)
    {
        var error = new Error(ex.GetType().Name, ex.Message);

        await WriteResult(context, error, statusCode);
    }

    private async Task WriteResult(HttpContext context, Error error, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        var problem = ErrorDetailsFactory.CreateProblemDetails("Exception", (int)statusCode, error);
        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }
}