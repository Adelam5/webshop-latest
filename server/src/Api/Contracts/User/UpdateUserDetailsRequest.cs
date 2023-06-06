namespace Api.Contracts.User;

public sealed record UpdateUserDetailsRequest(
    string FirstName,
    string LastName,
    string Email)
{
    public string? Id { get; set; }
};
