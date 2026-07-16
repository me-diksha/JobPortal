using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Models.Responses;

namespace JobPortalAPI.Repositories.Abstractions
{
    public interface ICandidateRepository
    {
        Task<int> CreateProfile(CandidateProfile request);

        Task<int> AddSkill(CandidateSkill skill);

        Task<int> AddEducation(CandidateEducation education);

        Task<int> AddExperience(CandidateExperience experience);

        Task<CandidateProfile?> GetProfile(int userId);
        Task<List<CandidateSkill>> GetSkills(int userId);
        Task<List<CandidateEducation>> GetEducation(int userId);
        Task<List<CandidateExperience>> GetExperience(int userId);
        Task<bool> DeleteSkill(int skillRecordId, int userId);

        Task<bool> DeleteEducation(int educationId, int userId);
        Task<bool> DeleteExperience(int experienceId, int userId);
        Task<CandidateEducation> UpdateEducation(CandidateEducation education, int educationid);
        Task<CandidateExperience> UpdateExperience(CandidateExperience experience);


    }
}
