using JobPortalAPI.DataAccess;
using JobPortalAPI.DataBaseAccess;
using JobPortalAPI.DataBaseAccess.Abstractions;
using JobPortalAPI.Repositories;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.Settings;

namespace JobPortalAPI.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration) {

            services.Configure<DBSettings>(configuration.GetSection("Database"));
            services.AddScoped<IDbAccess, DbAccess>();
            
            services.AddScoped<IDbExecutor, DbExecutor>();
            services.AddScoped<IAuthRepository,AuthRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IRecruiterRepository, RecruiterRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            return services;
        }
    }
}
