namespace Api.Contracts.Authentication;

public sealed record LoginUserRequest(
    string Email,
    string Password);