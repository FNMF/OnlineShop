namespace API.Domain.Events.MerchantCase
{
    public class ShopAdminLoginEvent:IDomainEvent
    {
        public Guid MerchantAdminUuid { get;}
        public DateTime OccurredOn { get;}
        public ShopAdminLoginEvent(Guid merchantAdminUuid)
        {
            MerchantAdminUuid = merchantAdminUuid;
            OccurredOn = DateTime.Now;
        }
    }
}
