namespace JobPortalAPI.Models.Request
{
    public class CompanyRequest
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Industry { get; set; }

        public string? Website { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public string? LogosUrl { get; set; }

        public int? CompanySize { get; set; }

        public int? FoundedYear { get; set; }
    }
}