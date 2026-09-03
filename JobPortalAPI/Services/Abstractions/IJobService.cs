using JobPortalAPI.Models.Common;

namespace JobPortalAPI.Services.Abstractions
{
    public interface IJobService
    {
        Task<long?> CreateJob(Job job);
    }
}
