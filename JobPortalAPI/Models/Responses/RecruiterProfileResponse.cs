namespace JobPortalAPI.Models.Responses
{
    public class RecruiterProfileResponse
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long? CompanyId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Designation { get; set; }

        public string? Department { get; set; }

        public string? Phone { get; set; }

        public string? LinkedinUrl { get; set; }
        public string? CompanyName { get; set; }
    }
}
