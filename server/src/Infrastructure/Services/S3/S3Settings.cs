namespace Infrastructure.Services.S3;
public class S3Settings
{
    public const string SectionName = "AwsS3";
    public string Key { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public string BucketName { get; init; } = null!;
}
