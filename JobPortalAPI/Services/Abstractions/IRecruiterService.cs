using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Models.Responses;

namespace JobPortalAPI.Services.Abstractions
{
    public interface IRecruiterService
    {
        Task<long> CreateProfile(RecruiterProfileRequest recruiter,int userid);

        Task<RecruiterProfileResponse> GetProfile(int userId);

        Task<long> UpdateProfile(RecruiterProfileRequest recruiter,int userid);

        Task<bool> DeleteProfile(int userId);
    }
}
