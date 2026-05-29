namespace JobPortalAPI.Models.Responses
{
    public class CandidateProfileResponse
    {
        public int Id { get; set; }

        public string? Headline { get; set; }

        public string? Bio { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public required string Country { get; set; }

        public decimal CurrentSalary { get; set; }

        public decimal ExpectedSalary { get; set; }

        public string? ResumeUrl { get; set; }
        public string Firstname { get; set; }
        public string? LastName { get; set; }
    }
}
