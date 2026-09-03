using JobPortalAPI.Repositories;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.Services;
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
           
            services.AddScoped<IRecruiterService, RecruiterService>();
            
            services.AddScoped<ICompanyService, CompanyService>();
            
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IJobService, JobService>();
            
            return services;
        }
    }
}
