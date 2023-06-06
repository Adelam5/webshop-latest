namespace Application.Common.DTOs;
public sealed record CreatedUserDto(string Id, string FirstName, string Email)
{
    public string? ConfirmationToken { get; set; }
}