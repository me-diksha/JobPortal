using JobPortalAPI.Common;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Models.Responses;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.Services.Abstractions;
using System.Runtime.CompilerServices;

namespace JobPortalAPI.Services.Implementations
{
    public class CandidateService : ICandidateService

    {
        private readonly ICandidateRepository _repository;

        public CandidateService(  ICandidateRepository repository) { 

            _repository = repository;
        }
        public async Task<int> CreateProfile(CreateCandidateProfileRequest request)
        {

        }

        public async Task<int> AddSkill(CandidateSkillRequest request)
        {

        }

        public async Task<int> AddEducation(CandidateEducationRequest request)
        {

        }

        public async Task<int> AddExperience(CandidateExperienceRequest request)
        {

        }
        public async Task<CandidateProfile?> GetProfile(int userId)
        {

        }

        public async Task<List<CandidateSkillResponse>> GetSkills(int userId)
        {

        }

        public async Task<List<CandidateEducation>> GetEducation(int userId)
        {

        }

        public async Task<List<CandidateExperience>> GetExperience(int userId)
        {

        }

        public async Task<bool> DeleteSkill(int skillRecordId,int userId)
        {

        }

        public async Task<bool> DeleteEducation(int educationId,int userId)
        {

        }
        public async Task<bool> DeleteExperience(int experienceId,int userId)
        {

        }

        public async Task<CandidateFullProfileResponse?> GetFullProfile(int userId)
        {
         var profile =
            await _repository.GetProfile(userId);
        if (profile == null)
            return null;
            var skills =
            await _repository.GetSkills(userId);

        var education =
            await _repository.GetEducation(userId);

        var experience =
            await _repository.GetExperience(userId);

        return new CandidateFullProfileResponse
        {
            Profile = profile,
            Skills = skills,
            Education = education,
            Experience = experience
        };
    }
    }
}
