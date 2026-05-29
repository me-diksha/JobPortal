namespace JobPortalAPI.Models.Requests
{
    public class CandidateExperienceRequest
    {
        public string CompanyName { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public bool CurrentlyWorking { get; set; }

        public string? Description { get; set; }
    }
}
