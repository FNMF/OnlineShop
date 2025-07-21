namespace API.Domain.Events.PlatformCase
{
    public class PlatformAdminLoginEvent : IDomainEvent
    {
        public int PlatformAdminAccount { get; }
        public DateTime OccurrendOn { get; }
        public PlatformAdminLoginEvent(int platformAdminAccount)
        {
            PlatformAdminAccount = platformAdminAccount;
            OccurrendOn = DateTime.Now;
        }
    }
}
