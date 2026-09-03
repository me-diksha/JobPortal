namespace JobPortalAPI.Models.Responses
{
    public class JobResponse
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string? Location { get; set; }
        public long? RefEmploymentType { get; set;  }
        public string? EmploymentType { get; set; }
        public string? ExperienceLevel { get; set; }

        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }

        public DateOnly? Deadline { get; set; }

        public string Status { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

    }
}
