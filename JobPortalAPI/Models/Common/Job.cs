namespace JobPortalAPI.Models.Common
{
    public class Job
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Location { get; set; }
        public long? RefEmploymentType { get; set; }
        public string? ExperienceLevel { get; set; }
        public decimal? MinSalary { get; set; } 
        public decimal? MaxSalary { get; set; } 
        public string? Status { get; set; }
        public DateOnly? Deadline { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }
}
