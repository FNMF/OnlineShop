using API.Domain.Enums;

namespace API.Domain.Events.ProductCase
{
    public class UpdateProductEvent : IDomainEvent
    {
        public byte[] AdminUuid { get; }
        public CurrentType CurrentType { get; }
        public byte[] ProductUuid { get; }
        public DateTime OccurrendOn { get; }
        public UpdateProductEvent(byte[] adminUuid, CurrentType currentType, byte[] productUuid)
        {
            OccurrendOn = DateTime.Now;
            AdminUuid = adminUuid;
            CurrentType = currentType;
            ProductUuid = productUuid;
        }
    }
}
