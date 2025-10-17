using API.Domain.Services.PaymentPart.Interfaces;
using API.Infrastructure.WechatPayV3;

namespace API.Domain.Events.PaymentCase
{
    public class PaymentMarkAsPaidEvent : IDomainEvent
    {
        public WechatTransaction WechatTransaction { get; }
        public DateTime OccurredOn { get; }
        public PaymentMarkAsPaidEvent(WechatTransaction wechatTransaction)
        {
            WechatTransaction = wechatTransaction;
            OccurredOn = DateTime.Now;
        }

    }
}
