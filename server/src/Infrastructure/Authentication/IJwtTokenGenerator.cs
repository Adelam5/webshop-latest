using Infrastructure.Identity.Entities;

namespace Infrastructure.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
