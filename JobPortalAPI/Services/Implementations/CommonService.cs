using JobPortalAPI.Models.Common;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.Services.Abstractions;

namespace JobPortalAPI.Services.Implementations
{
    public class CommonService:ICommonService
    {
        private ICommonRepository _repository;
        public CommonService(ICommonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Skills>>GetAllSkills()
        {
            return await _repository.GetAllSkills();
        }
        public async Task<IEnumerable<EmploymentType>> GetAllEmploymentType()
        {
            return await _repository.GetAllEmploymentType();
        }
        public async Task<IEnumerable<ExperienceLevel>> GetAllExperienceLevel()
        {
            return await _repository.GetAllExperienceLevel();
        }
        public async Task<IEnumerable<JobStatus>> GetAllJobStatus()
        {
            return await _repository.GetAllJobStatus();
        }

    }
}
