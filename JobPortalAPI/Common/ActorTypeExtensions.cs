namespace JobPortalAPI.Common
{
    public static class ActorTypeExtensions
    {
        public static string ToClaim(this ActorType actorType) =>
            actorType switch
            {
                ActorType.Candidate => "candidate",
                ActorType.Recruiter => "recruiter",
                ActorType.Admin =>"admin",
                _ => throw new ArgumentOutOfRangeException(nameof(actorType), "Invalid actor type")
            };

        public static ActorType FromClaim(string claim) =>
            claim.ToLower() switch
            {
                "candidate" => ActorType.Candidate,
                "recruiter" => ActorType.Recruiter,
                "admin" => ActorType.Admin,
                _ => throw new ArgumentOutOfRangeException(nameof(claim), "Invalid actor type claim")
            };
    }
}
