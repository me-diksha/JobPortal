namespace JobPortalAPI.Models.Requests
{
    public class CandidateExperienceRequest
    {
        public int UserId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool CurrentlyWorking { get; set; }

        public string? Description { get; set; }
    }
}
