namespace JobPortalAPI.Models.Requests
{
    public class CandidateSkillRequest
    {
        public int UserId { get; set; }

        public int SkillId { get; set; }

        public int ExperienceYears { get; set; }
    }
}
