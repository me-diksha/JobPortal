using JobPortalAPI.Models.Common;

namespace JobPortalAPI.Repositories.Abstractions
{
    public interface ICommonRepository
    {
        Task<IEnumerable<Skills>> GetAllSkills();
    }
}
