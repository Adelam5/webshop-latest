using Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Api.UnitTests.Middleware;

public class ExceptionHandlerMiddlewareTests : IClassFixture<ExceptionHandlerMiddlewareFixture>
{
    private readonly ExceptionHandlerMiddlewareFixture fixture;

    public ExceptionHandlerMiddlewareTests(ExceptionHandlerMiddlewareFixture fixture)
    {
        this.fixture = fixture;
    }

    private async Task<ProblemDetails?> DeserializeProblemDetailsAsync(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ProblemDetails>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    [Fact]
    public async Task Invoke_ThrowsException_Returns500WithProblemDetails()
    {
        // Arrange
        fixture.ConfigureApp(app => app.Run(context => throw new Exception("Test Exception")));

        // Act
        var response = await fixture.Client.GetAsync("/");
        var problemDetails = await DeserializeProblemDetailsAsync(response);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        problemDetails.Detail.Should().Be("Test Exception");
        problemDetails.Type.Should().Be("Exception");
        problemDetails.Status.Should().Be((int)HttpStatusCode.InternalServerError);
        problemDetails.Title.Should().Be("Exception");
        problemDetails.Extensions.Should().BeOfType<Dictionary<string, object>>();
        problemDetails.Extensions["errors"].Should().BeNull();
    }

    [Fact]
    public async Task Invoke_ThrowsAppException_Returns500WithProblemDetails()
    {
        // Arrange
        fixture.ConfigureApp(app => app.Run(context => throw new AppException("Test AppException")));

        // Act
        var response = await fixture.Client.GetAsync("/");
        var problemDetails = await DeserializeProblemDetailsAsync(response);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        problemDetails.Detail.Should().Be("Test AppException");
        problemDetails.Type.Should().Be("AppException");
        problemDetails.Status.Should().Be((int)HttpStatusCode.InternalServerError);
        problemDetails.Title.Should().Be("Exception");
        problemDetails.Extensions.Should().BeOfType<Dictionary<string, object>>();
        problemDetails.Extensions["errors"].Should().BeNull();
    }

    [Fact]
    public async Task Invoke_ThrowsUnauthenticatedException_Returns401WithProblemDetails()
    {
        // Arrange
        fixture.ConfigureApp(app => app.Run(context => throw new UnauthenticatedException()));

        // Act
        var response = await fixture.Client.GetAsync("/");
        var problemDetails = await DeserializeProblemDetailsAsync(response);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        problemDetails.Detail.Should().Be("User is not logged in");
        problemDetails.Type.Should().Be("UnauthenticatedException");
        problemDetails.Status.Should().Be((int)HttpStatusCode.Unauthorized);
        problemDetails.Title.Should().Be("Exception");
        problemDetails.Extensions.Should().BeOfType<Dictionary<string, object>>();
        problemDetails.Extensions["errors"].Should().BeNull();
    }
}