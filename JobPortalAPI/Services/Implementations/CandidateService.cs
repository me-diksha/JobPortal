using JobPortalAPI.Common;
using JobPortalAPI.Extensions;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Models.Responses;
using JobPortalAPI.Repositories;
using JobPortalAPI.Repositories.Abstractions;
using JobPortalAPI.Services.Abstractions;
using System.Runtime.CompilerServices;
using AutoMapper;

namespace JobPortalAPI.Services.Implementations
{
    public class CandidateService : ICandidateService

    {
        private readonly ICandidateRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CandidateService> _logger;

        public CandidateService(  ICandidateRepository repository,IMapper mappingProfile, ILogger<CandidateService> logger) { 

            _repository = repository;
            _mapper = mappingProfile;
            _logger = logger;
        }
        public async Task<int> CreateProfile(int userid,CreateCandidateProfileRequest request)
        {
            var profile = new CandidateProfile
            {
                UserId = userid,
                Headline = request.Headline,
                Bio = request.Bio,
                CurrentSalary = request.CurrentSalary,
                ExpectedSalary = request.ExpectedSalary,
                ResumeUrl = request.ResumeUrl,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                City = request.City,
                State = request.State,
                Country = request.Country,
                Firstname= request.Firstname,
                LastName=request.LastName
            };

            return await _repository.CreateProfile(profile);
        }

        public async Task<int> AddSkill(int userid,CandidateSkillRequest request)
        {
            try
            {
                if (request.SkillId <= 0)
                    throw new ArgumentException("Invalid skill id");
                var addskill = new CandidateSkill
                {
                    UserId = userid,
                    SkillId = request.SkillId,
                    ExperienceYears = request.ExperienceYears
                };

                return await _repository.AddSkill(addskill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in service AddSkill");
                throw;
            }

        }

        public async Task<int> AddEducation(int userid,CandidateEducationRequest request)
        {
            try
            {
                if (request.StartYear > request.EndYear)
                    throw new ArgumentException("Start year cannot be greater than end year.");
                var addeducation = new CandidateEducation
                {
                    UserId = userid,
                    EndYear = request.EndYear,
                    StartYear = request.StartYear,
                    InstituteName = request.InstituteName,
                    FieldOfStudy = request.FieldOfStudy,
                    Degree = request.Degree,
                    Percentage = request.Percentage
                };

                return await _repository.AddEducation(addeducation);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error in AddEducation");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service error in AddEducation");
                throw ;
            }
        }

        public async Task<int> AddExperience(int userid,CandidateExperienceRequest request)
        {
            try
            {
                if (request.StartDate > request.EndDate && request.CurrentlyWorking == false)
                    throw new ArgumentException("Start date cannot be greater than end date.");
                var addexp = new CandidateExperience
                {
                    UserId = userid,
                    CompanyName = request.CompanyName,
                    Designation = request.Designation,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    CurrentlyWorking = request.CurrentlyWorking,
                    Description = request.Description
                };
                return await _repository.AddExperience(addexp);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error in AddExperience");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service error in AddExperience");
                throw;
            }
        }
        public async Task<CandidateProfile?> GetProfile(int userid)
        {
            try
            {
                return await _repository.GetProfile(userid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetProfile");
                throw;
            }
        }

        public async Task<List<CandidateSkillResponse>> GetSkills(int userid)
        {
            try
            {
                var skills = await _repository.GetSkills(userid);
                return _mapper.Map<List<CandidateSkillResponse>>(skills);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSkills");
                throw;
            }
        }

        public async Task<List<CandidateEducationResponse>> GetEducation(int userId)
        {
            try
            {
                var education = await _repository.GetEducation(userId);
                return _mapper.Map<List<CandidateEducationResponse>>(education);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEducation");
                throw;
            }
        }

        public async Task<List<CandidateExperienceResponse>> GetExperience(int userId)
        {
            try
            {
                var expreience = await _repository.GetExperience(userId);
                return _mapper.Map<List<CandidateExperienceResponse>>(expreience);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetExperience");
                throw;
            }
        }

        public async Task<bool> DeleteSkill(int skillRecordId,int userId)
        {
            try
            {
                var result = await _repository.DeleteSkill(skillRecordId, userId);
                if (!result)
                    throw new KeyNotFoundException("Skill record not found.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting skill");
                throw;
            }
 
        }

        public async Task<bool> DeleteEducation(int educationId,int userId)
        {
            try
            {
                var result = await _repository.DeleteEducation(educationId, userId);

                if (!result)
                    throw new KeyNotFoundException("Education record not found.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting education");
                throw;
            }
            
        }
        public async Task<bool> DeleteExperience(int experienceId,int userId)
        {
            try
            {
                var result = await _repository.DeleteExperience(experienceId, userId);

                if (!result)
                    throw new KeyNotFoundException("Experience record not found.");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting experience");
                throw;
            }

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
            Profile = _mapper.Map<CandidateProfileResponse>(profile),

            Skills = _mapper.Map<List<CandidateSkillResponse>>(skills),

            Education = _mapper.Map<List<CandidateEducationResponse>>(education),

            Experience = _mapper.Map<List<CandidateExperienceResponse>>(experience)
        };
    }
    }
}
