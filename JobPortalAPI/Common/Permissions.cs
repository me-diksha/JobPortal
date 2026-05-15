namespace JobPortalAPI.Common
{
    public static class Permissions
    {
        // Candidate
        public const string ApplyJob = "job.apply";
        public const string EditProfile = "profile.edit";

        // Recruiter
        public const string CreateJob = "job.create";
        public const string EditJob = "job.edit";
        public const string ViewApplications = "application.view";

        // Admin
        public const string ManageUsers = "user.manage";
    }
}
