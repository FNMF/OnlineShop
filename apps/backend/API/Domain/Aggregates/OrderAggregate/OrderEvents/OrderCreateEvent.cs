using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Events;

namespace API.Domain.Aggregates.OrderAggregate.OrderEvents
{
    public class OrderCreateEvent: IDomainEvent
    {
        public OrderMain OrderMain { get; }
        public Guid MerchantUuid { get; }
        public DateTime OccurredOn { get; }
        public OrderCreateEvent(OrderMain orderMain, Guid merchantUuid)
        {
            OrderMain = orderMain;
            MerchantUuid = merchantUuid;
            OccurredOn = DateTime.Now;
        }
    }
}
