namespace JobPortalAPI.Models.Requests
{
    public class JobRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Location { get; set; }
        public long? RefEmploymentType { get; set; }
        public long? RefExperienceLevel { get; set; }
        public decimal? MinSalary { get; set; } 
        public decimal? MaxSalary { get; set; } 
        public DateOnly? Deadline { get; set; }
    }
}
