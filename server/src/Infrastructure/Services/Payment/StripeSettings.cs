namespace Infrastructure.Services.Payment;
public class StripeSettings
{
    public const string SectionName = "StripeSettings";
    public string PublishableKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
}
