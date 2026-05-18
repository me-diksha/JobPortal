using JobPortalAPI.Settings;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.DataBaseAccess;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.DataAccess;

namespace JobPortalAPI.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration) {

            services.Configure<DBSettings>(configuration.GetSection("Database"));
            services.AddScoped<IDbAccess, DbAccess>();
            services.AddScoped<IAuthRepository,AuthRepository>();
            return services;
        }
    }
}
