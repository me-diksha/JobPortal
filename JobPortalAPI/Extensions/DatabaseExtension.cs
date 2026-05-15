using JobPortalAPI.Settings;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.DataBaseAccess;

namespace JobPortalAPI.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration) {

            services.Configure<DBSettings>(configuration.GetSection("Database"));
            services.AddScoped<IDbAccess, DbAccess>();
            return services;
        }
    }
}
