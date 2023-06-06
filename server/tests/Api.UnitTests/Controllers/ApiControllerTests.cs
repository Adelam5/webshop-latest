using Domain.Errors;
using Domain.Primitives.Result;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.UnitTests.Controllers;

public class ApiControllerTests
{
    private readonly TestApiController controller;

    public ApiControllerTests()
    {
        controller = new TestApiController();
    }

    [Fact]
    public void HandleResult_Success_ReturnsOkResult()
    {
        // Arrange
        var result = Result.Success("Test Success");

        // Act
        var actionResult = controller.PublicHandleResult(result);

        // Assert
        actionResult.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public void HandleResult_Failure_Returns400WithProblemDetails()
    {
        // Arrange
        var result = Result.Failure<string>(new Error("TestFailure", "Testing Result Failure"));

        // Act
        var actionResult = controller.PublicHandleResult(result);
        var objectResult = (BadRequestObjectResult)actionResult;

        // Assert
        actionResult.Should().BeOfType<BadRequestObjectResult>();
        objectResult.Value.Should().BeOfType<ProblemDetails>();
    }

    [Fact]
    public void HandleResult_ValidationErrors_Returns400WithProblemDetails()
    {
        // Arrange
        var validationErrors = new Error[]
        {
            new Error("Field1", "Error1"),
            new Error("Field1", "Error2"),
            new Error("Field2", "Error3")
        };
        var validationResult = (Result<string>)ValidationResult<string>.WithErrors(validationErrors);

        // Act
        var actionResult = controller.PublicHandleResult(validationResult);
        var objectResult = (BadRequestObjectResult)actionResult;

        // Assert
        actionResult.Should().BeOfType<BadRequestObjectResult>();
        objectResult.Value.Should().BeOfType<ProblemDetails>();
    }

    [Fact]
    public void HandleResult_NotAuthenticated_Returns401WithProblemDetails()
    {
        // Arrange
        var result = Result.Failure<string>(Errors.Authentication.NotAuthenticated);

        // Act
        var actionResult = controller.PublicHandleResult(result);
        var objectResult = (UnauthorizedObjectResult)actionResult;
        var problemDetails = objectResult.Value as ProblemDetails;

        // Assert
        actionResult.Should().BeOfType<UnauthorizedObjectResult>();
        objectResult.Value.Should().BeOfType<ProblemDetails>();
        problemDetails.Status.Should().Be((int)HttpStatusCode.Unauthorized);
        problemDetails.Title.Should().Be("Not Authenticated");
        problemDetails.Type.Should().Be("Auth.NotAuthenticated");
        problemDetails.Detail.Should().Be("User is not authenticated.");
        problemDetails.Extensions["errors"].Should().BeNull();
    }

}