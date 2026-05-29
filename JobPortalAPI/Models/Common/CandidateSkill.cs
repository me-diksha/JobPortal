namespace JobPortalAPI.Models.Common
{
    public class CandidateSkill
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int SkillId { get; set; }

        public string SkillName { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
    }
}
