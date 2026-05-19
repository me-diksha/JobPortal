namespace JobPortalAPI.Models.Responses
{
    public class CandidateFullProfileResponse
    {
        public CandidateProfileResponse? Profile { get; set; }

        public List<CandidateSkillResponse> Skills { get; set; } = new();

        public List<CandidateEducationResponse> Education { get; set; } = new();

        public List<CandidateExperienceResponse> Experience { get; set; } = new();
    }
}
