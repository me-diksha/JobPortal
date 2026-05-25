using JobPortalAPI.Models.Common;
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
        Task<CandidateProfile?> GetProfile(int userId);

        Task<List<CandidateSkillResponse>> GetSkills(int userId);

        Task<List<CandidateEducationResponse>> GetEducation(int userId);

        Task<List<CandidateExperienceResponse>> GetExperience(int userId);
        Task<CandidateFullProfileResponse?> GetFullProfile(int userId);

        Task<bool> DeleteSkill(int skillRecordId,int userId);

        Task<bool> DeleteEducation(int educationId,int userId);
        Task<bool> DeleteExperience(int experienceId, int userId);

    }
}
