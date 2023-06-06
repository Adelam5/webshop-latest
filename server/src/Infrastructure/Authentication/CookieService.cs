using Domain.Abstractions.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication;
public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IDateTimeProvider dateTimeProvider;

    public CookieService(IHttpContextAccessor httpContextAccessor, IDateTimeProvider dateTimeProvider)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.dateTimeProvider = dateTimeProvider;
    }

    public void SetTokenInCookie(string token)
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Append("token", token,
             new CookieOptions
             {
                 Expires = dateTimeProvider.UtcNow.AddDays(20),
                 HttpOnly = true,
                 Secure = true,
                 IsEssential = true,
                 SameSite = SameSiteMode.None
             });
    }

    public void RemoveTokenFromCookie()
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Delete("token");
    }
}
