using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Models.Responses;

namespace JobPortalAPI.Services.Abstractions
{
    public interface IJobService
    {
        Task<long> CreateJob(
            int userId,
            JobRequest request);

        Task<JobResponse?> GetJob(
            long id);

        Task<IEnumerable<JobResponse>> GetAllJobs();

        Task<IEnumerable<JobResponse>> GetRecruiterJobs(
            int userId);
        Task<bool> UpdateJobStatus(long jobId, int userId, int statusId);
        Task<JobResponse> UpdateJob(
            long id,
            int userId,
            JobRequest request);

        Task<bool> DeleteJob(
            long id,
            int userId);
    }
}