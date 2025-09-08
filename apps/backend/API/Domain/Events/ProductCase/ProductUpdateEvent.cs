using API.Domain.Enums;

namespace API.Domain.Events.ProductCase
{
    public class ProductUpdateEvent : IDomainEvent
    {
        public Guid AdminUuid { get; }
        public CurrentType CurrentType { get; }
        public Guid ProductUuid { get; }
        public DateTime OccurredOn { get; }
        public ProductUpdateEvent(Guid adminUuid, CurrentType currentType, Guid productUuid)
        {
            OccurredOn = DateTime.Now;
            AdminUuid = adminUuid;
            CurrentType = currentType;
            ProductUuid = productUuid;
        }
    }
}
