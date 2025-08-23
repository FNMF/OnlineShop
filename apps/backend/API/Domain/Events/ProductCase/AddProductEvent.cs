using API.Domain.Enums;

namespace API.Domain.Events.ProductCase
{
    public class AddProductEvent : IDomainEvent
    {
        public Guid AdminUuid { get; }
        public CurrentType CurrentType { get; }
        public Guid ProductUuid { get; }
        public DateTime OccurrendOn { get; }
        public AddProductEvent(Guid adminUuid,CurrentType currentType, Guid productUuid)
        {
            OccurrendOn = DateTime.Now;
            AdminUuid = adminUuid;
            CurrentType = currentType;
            ProductUuid = productUuid;
        }
    }
}
