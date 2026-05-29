using AutoMapper;
using JobPortalAPI.Models.Common;
using JobPortalAPI.Models.Requests;
using JobPortalAPI.Models.Responses;
namespace JobPortalAPI.Extensions
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {

            // Request -> Entity
            CreateMap<CandidateSkillRequest, CandidateSkill>();

            // Entity -> Response
            CreateMap<CandidateSkill, CandidateSkillResponse>();

            CreateMap<CandidateEducationRequest, CandidateEducation>();

            // Entity -> Response
            CreateMap<CandidateEducation, CandidateEducationResponse>();

            CreateMap<CandidateExperienceRequest, CandidateExperience>();

            // Entity -> Response
            CreateMap<CandidateExperience, CandidateExperienceResponse>();

            CreateMap<CandidateProfile, CandidateProfileResponse>();
        }
    }
}
