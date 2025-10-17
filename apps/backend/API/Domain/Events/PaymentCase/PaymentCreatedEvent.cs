using API.Domain.Aggregates.OrderAggregates;

namespace API.Domain.Events.PaymentCase
{
    public class PaymentCreatedEvent : IDomainEvent
    {
        public OrderMain orderMain { get; }
        public string appId { get; }
        public string mchId { get; }
        public string openId { get; }
        public DateTime OccurredOn { get; }
        public PaymentCreatedEvent(
            OrderMain orderMain,
            string appId,
            string mchId,
            string openId
            )
        {
            this.orderMain = orderMain;
            this.appId = appId;
            this.mchId = mchId;
            this.openId = openId;
            OccurredOn = DateTime.Now;
        }
    }
}
