namespace JobPortalAPI.Models.Requests
{
    public class RecruiterProfileRequest
    {
        public long? CompanyId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Designation { get; set; }

        public string? Department { get; set; }

        public string? Phone { get; set; }

        public string? LinkedinUrl { get; set; }
    }
}
