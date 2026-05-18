using JobPortalAPI.Common;
using JobPortalAPI.Services.Abstractions;
using JobPortalAPI.Services.Implementations;
using JobPortalAPI.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JobPortalAPI.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.SecretKey)
                        )
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.CandidateOnly, policy =>
                    policy.RequireClaim("actor_type", ActorType.Candidate.ToClaim()));

                options.AddPolicy(PolicyNames.RecruiterOnly, policy =>
                    policy.RequireClaim("actor_type", ActorType.Recruiter.ToClaim()));

                options.AddPolicy(PolicyNames.AdminOnly, policy =>
                    policy.RequireClaim("actor_type", ActorType.Admin.ToClaim()));

                var allPermissions = typeof(Permissions).GetFields(System.Reflection.BindingFlags.Public |
                  System.Reflection.BindingFlags.Static |
                  System.Reflection.BindingFlags.FlattenHierarchy)
                 .Where(f => f.IsLiteral && !f.IsInitOnly)
                 .Select(f => f.GetValue(null)?.ToString())
                 .Where(v => v != null);

                foreach (var permission in allPermissions)
                    options.AddPolicy(permission!, p =>
                        p.AddRequirements(new PermissionRequirement(permission!)));
            });
            return services;
        }
    }
}
