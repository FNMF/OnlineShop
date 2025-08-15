namespace API.Domain.Events.MerchantCase
{
    public class MerchantUpdateProductEvent:IDomainEvent
    {
        public byte[] MerchantAdminUuid { get; }
        public byte[] ProductUuid { get; }
        public DateTime OccurrendOn { get; }
        public MerchantUpdateProductEvent(byte[] merchantAdminUuid, byte[] productUuid)
        {
            OccurrendOn = DateTime.Now;
            MerchantAdminUuid = merchantAdminUuid;
            ProductUuid = productUuid;
        }
    }
}
