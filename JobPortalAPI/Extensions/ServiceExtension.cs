using JobPortalAPI.Repositories;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.Services.Abstractions;
using JobPortalAPI.Services.Implementations;
using JobPortalAPI.Settings;

namespace JobPortalAPI.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<ICandidateRepository,CandidateRepository>();
            return services;
        }
    }
}
