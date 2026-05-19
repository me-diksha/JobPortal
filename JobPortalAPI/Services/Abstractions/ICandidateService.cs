using JobPortalAPI.Models.Requests;
using JobPortalAPI.Models.Responses;

namespace JobPortalAPI.Services.Abstractions
{
    public interface ICandidateService
    {
        Task<int> CreateProfile(CreateCandidateProfileRequest request);

        Task<int> AddSkill(CandidateSkillRequest request);

        Task<int> AddEducation(CandidateEducationRequest request);

        Task<int> AddExperience(CandidateExperienceRequest request);

        Task<CandidateFullProfileResponse?> GetFullProfile(int userId);
    }
}
