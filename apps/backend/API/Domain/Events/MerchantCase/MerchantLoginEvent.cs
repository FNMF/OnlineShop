namespace API.Domain.Events.MerchantCase
{
    public class MerchantLoginEvent:IDomainEvent
    {
        public Guid MerchantAdminUuid { get;}
        public DateTime OccurredOn { get;}
        public MerchantLoginEvent(Guid merchantAdminUuid)
        {
            MerchantAdminUuid = merchantAdminUuid;
            OccurredOn = DateTime.Now;
        }
    }
}
