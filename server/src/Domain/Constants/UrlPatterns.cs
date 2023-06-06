namespace Domain.Constants;
public static class UrlPatterns
{
    public const string ImageUrlPattern =
        @"^https?:\/\/([a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,6}(\/[^\s]*\.(?:jpe?g|png))?$";

}
