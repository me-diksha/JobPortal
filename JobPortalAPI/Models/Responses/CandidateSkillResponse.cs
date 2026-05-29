namespace JobPortalAPI.Models.Responses
{
    public class CandidateSkillResponse
    {
        public int Id { get; set; }

        public int SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;

        public int ExperienceYears { get; set; }
    }
}
