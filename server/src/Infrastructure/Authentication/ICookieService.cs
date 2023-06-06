namespace Infrastructure.Authentication;
public interface ICookieService
{
    void SetTokenInCookie(string token);
    void RemoveTokenFromCookie();
}
