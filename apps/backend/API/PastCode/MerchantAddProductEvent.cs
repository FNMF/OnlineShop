using API.Domain.Events;

namespace API.PastCode
{
    public class MerchantAddProductEvent:IDomainEvent
    {
        public byte[] MerchantAdminUuid { get; }
        public byte[] ProductUuid { get; }
        public DateTime OccurrendOn { get; }
        public MerchantAddProductEvent(byte[] merchantAdminUuid, byte[] productUuid)
        {
            OccurrendOn = DateTime.Now;
            MerchantAdminUuid = merchantAdminUuid;
            ProductUuid = productUuid;
        }
    }
}
