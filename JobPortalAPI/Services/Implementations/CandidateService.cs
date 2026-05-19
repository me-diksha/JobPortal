using JobPortalAPI.Models.Requests;
using JobPortalAPI.Models.Responses;
using JobPortalAPI.Services.Abstractions;

namespace JobPortalAPI.Services.Implementations
{
    public class CandidateService:ICandidateService

    {

        public CandidateService() { }
        Task<int> CreateProfile(CreateCandidateProfileRequest request);

        Task<int> AddSkill(CandidateSkillRequest request);

        Task<int> AddEducation(CandidateEducationRequest request);

        Task<int> AddExperience(CandidateExperienceRequest request);

        Task<CandidateFullProfileResponse?> GetFullProfile(int userId);
    }
}
