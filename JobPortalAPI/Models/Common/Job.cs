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
        public long? RefExperienceLevel { get; set; }
        public long? RefStatus { get; set; }
        public decimal? MinSalary { get; set; } 
        public decimal? MaxSalary { get; set; } 
        public DateOnly? Deadline { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }
}
