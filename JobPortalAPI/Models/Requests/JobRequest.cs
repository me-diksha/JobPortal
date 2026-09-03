namespace JobPortalAPI.Models.Requests
{
    public class JobRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Location { get; set; }
        public long? RefEmploymentType { get; set; }
        public string? ExperienceLevel { get; set; }
        public decimal? MinSalary { get; set; } = 0;
        public decimal? MaxSalary { get; set; } = 0;
        public DateOnly? Deadline { get; set; }
    }
}
