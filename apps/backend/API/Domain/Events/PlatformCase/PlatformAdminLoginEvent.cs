namespace API.Domain.Events.PlatformCase
{
    public class PlatformAdminLoginEvent : IDomainEvent
    {
        public int PlatformAdminAccount { get; }
        public DateTime OccurredOn { get; }
        public PlatformAdminLoginEvent(int platformAdminAccount)
        {
            PlatformAdminAccount = platformAdminAccount;
            OccurredOn = DateTime.Now;
        }
    }
}
