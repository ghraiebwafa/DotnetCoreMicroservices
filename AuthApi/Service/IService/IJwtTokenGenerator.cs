using AuthApi.Models;

namespace AuthApi;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser applicationUser);
}