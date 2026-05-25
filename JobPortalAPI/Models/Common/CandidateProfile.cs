namespace JobPortalAPI.Models.Common
{
    public class CandidateProfile
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string? Headline { get; set; }

        public string? Bio { get; set; }

        public string? Location { get; set; }

        public decimal CurrentSalary { get; set; }

        public decimal ExpectedSalary { get; set; }

        public string? ResumeUrl { get; set; }
    }
}
