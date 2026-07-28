using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Requests;

namespace JobPortalAPI.Repositories.Abstractions
{
    public interface IRecruiterRepository
    {
        Task<long> CreateProfile(Recruiter recruiter);

        Task<Recruiter?> GetProfile(int userId);

        Task<long> UpdateProfile(Recruiter recruiter);

        Task<bool> DeleteProfile(int userId);
 
    }
}
