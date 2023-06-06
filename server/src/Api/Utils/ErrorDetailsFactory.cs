using Domain.Primitives.Result;
using Microsoft.AspNetCore.Mvc;

namespace Api.Utils;

public static class ErrorDetailsFactory
{
    public static ProblemDetails CreateProblemDetails(
    string title, int status, Error error, Error[]? errors = null)
    => new()
    {
        Title = title,
        Type = error.Code,
        Detail = error.Message,
        Status = status,
        Extensions = { { nameof(errors), errors } }
    };
}
