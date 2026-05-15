using JobPortalAPI.Services.Abstractions;
using JobPortalAPI.Settings;
using log4net;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using SymmetricKey = Microsoft.IdentityModel.Tokens.SymmetricSecurityKey;

namespace JobPortalAPI.Services.Implementations
{
    public class JwtService: IJwtService
    {
        private readonly ILogger _logger;
        private readonly JwtSettings _jwtSettings;
        public JwtService(ILogger<JwtService> logger, IOptions<JwtSettings> jwtSettings) { 
         
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        public string GenerateAccessToken(long Id, string Email, string Login, string actorType, List<string>? permissions = null)
        {
            try
            {

                var key = new SymmetricKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                     new Claim(JwtRegisteredClaimNames.Sub, Id.ToString()),
                     new Claim(JwtRegisteredClaimNames.Email, Email ?? ""),
                     new Claim(JwtRegisteredClaimNames.UniqueName,Login ?? string.Empty),
                     new Claim("actor_type", actorType),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                };

                if (permissions != null)
                {
                    foreach (var permission in permissions)
                    {
                        claims.Add(new Claim("permission", permission));
                    }
                }
                var token = new JwtSecurityToken(
                       issuer: _jwtSettings.Issuer,
                       audience: _jwtSettings.Audience,
                       claims: claims,
                       expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes),
                       signingCredentials: credentials
                );

                _logger.LogInformation("Access token generated. Id: {Id}, ActorType: {ActorType}, Permissions: {Count}",
                                 Id, actorType, permissions?.Count ?? 0);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token generation failed: {ex.Message}");
                throw;

            }
            
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var key = new SymmetricKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = key
            };

            var handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(token, validationParams, out var securityToken);

            if (securityToken is not JwtSecurityToken jwt ||
                !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

    }
}
