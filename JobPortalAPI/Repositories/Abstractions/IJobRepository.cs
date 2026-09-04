using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Responses;

namespace JobPortalAPI.Repositories.Abstractions
{
    public interface IJobRepository
    {
        Task<long> CreateJob(Job job);

        Task<JobResponse?> GetJob(long id);

        Task<IEnumerable<JobResponse>> GetAllJobs();

        Task<IEnumerable<JobResponse>> GetRecruiterJobs(
            long companyId);

        Task<JobResponse> UpdateJob(Job job);

        Task<bool> DeleteJob(
            long id,
            long companyId,
            long updatedBy);
    }
}