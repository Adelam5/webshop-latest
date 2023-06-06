namespace Api.Contracts.Authentication;

public sealed record VerifyEmailRequest(
    string Token,
    string UserId);
