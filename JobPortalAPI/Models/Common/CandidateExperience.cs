namespace JobPortalAPI.Models.Common
{
    public class CandidateExperience
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public bool CurrentlyWorking { get; set; }

        public string? Description { get; set; }
    }
}
