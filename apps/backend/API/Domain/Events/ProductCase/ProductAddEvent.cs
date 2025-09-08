using API.Domain.Enums;

namespace API.Domain.Events.ProductCase
{
    public class ProductAddEvent : IDomainEvent
    {
        public Guid AdminUuid { get; }
        public CurrentType CurrentType { get; }
        public Guid ProductUuid { get; }
        public DateTime OccurredOn { get; }
        public ProductAddEvent(Guid adminUuid,CurrentType currentType, Guid productUuid)
        {
            OccurredOn = DateTime.Now;
            AdminUuid = adminUuid;
            CurrentType = currentType;
            ProductUuid = productUuid;
        }
    }
}
