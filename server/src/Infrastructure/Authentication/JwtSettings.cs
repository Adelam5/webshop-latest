namespace Infrastructure.Authentication;
public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Secret { get; set; } = null!;
    public int ExpiryDays { get; init; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;

}


