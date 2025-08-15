namespace API.Domain.Events.MerchantCase
{
    public class MerchantLoginEvent:IDomainEvent
    {
        public byte[] MerchantAdminUuid { get;}
        public DateTime OccurrendOn { get;}
        public MerchantLoginEvent(byte[] merchantAdminUuid)
        {
            MerchantAdminUuid = merchantAdminUuid;
            OccurrendOn = DateTime.Now;
        }
    }
}
