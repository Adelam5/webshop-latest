namespace Infrastructure.Services.Email;
internal class SendGridSettings
{
    public const string SectionName = "SendGridSettings";
    public string ApiKey { get; set; } = null!;
    public string FromEmail { get; init; } = null!;
    public string AdminEmail { get; init; } = null!;
}
