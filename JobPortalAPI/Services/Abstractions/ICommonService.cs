using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Responses;
using JobPortalAPI.Repositories.Abstractions;

namespace JobPortalAPI.Services.Abstractions
{
    public interface ICommonService
    {
        Task<IEnumerable<Skills>> GetAllSkills();
        Task<IEnumerable<EmploymentType>> GetAllEmploymentType();
    }
}
