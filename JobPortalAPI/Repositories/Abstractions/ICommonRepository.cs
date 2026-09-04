using JobPortalAPI.Models.Common;

namespace JobPortalAPI.Repositories.Abstractions
{
    public interface ICommonRepository
    {
        Task<IEnumerable<Skills>> GetAllSkills();
        Task<IEnumerable<EmploymentType>> GetAllEmploymentType();
        Task<IEnumerable<ExperienceLevel>> GetAllExperienceLevel();
        Task<IEnumerable<JobStatus>> GetAllJobStatus();
    }
}
