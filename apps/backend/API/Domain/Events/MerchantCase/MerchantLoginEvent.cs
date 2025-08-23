namespace API.Domain.Events.MerchantCase
{
    public class MerchantLoginEvent:IDomainEvent
    {
        public Guid MerchantAdminUuid { get;}
        public DateTime OccurrendOn { get;}
        public MerchantLoginEvent(Guid merchantAdminUuid)
        {
            MerchantAdminUuid = merchantAdminUuid;
            OccurrendOn = DateTime.Now;
        }
    }
}
