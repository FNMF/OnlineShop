namespace API.Domain.Events.MerchantCase
{
    public class MerchantRemoveProductEvent:IDomainEvent
    {
        public byte[] MerchantAdminUuid { get; }
        public byte[] ProductUuid { get; }
        public DateTime OccurrendOn { get; }
        public MerchantRemoveProductEvent(byte[] merchantAdminUuid, byte[] productUuid)
        {
            OccurrendOn = DateTime.Now;
            MerchantAdminUuid = merchantAdminUuid;
            ProductUuid = productUuid;
        }
    }
}
