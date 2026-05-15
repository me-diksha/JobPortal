using System.Security.Claims;

namespace JobPortalAPI.Services.Abstractions

{
    public interface IJwtService
    {
        string GenerateAccessToken(long Id, string Email, string Login, string actorType, List<string>? permissions = null);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
