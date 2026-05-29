namespace JobPortalAPI.Models.Requests
{
    public class CandidateEducationRequest
    {

        public string InstituteName { get; set; } = string.Empty;

        public string Degree { get; set; } = string.Empty;

        public string? FieldOfStudy { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public decimal Percentage { get; set; }
    }
}
