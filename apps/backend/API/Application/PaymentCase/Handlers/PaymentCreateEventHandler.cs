using API.Application.Common.EventBus;
using API.Domain.Events.PaymentCase;
using API.Domain.Services.PaymentPart.Interfaces;

namespace API.Application.PaymentCase.Handlers
{
    public class PaymentCreateEventHandler:IEventHandler<PaymentCreatedEvent>
    {
        private readonly IPaymentCreateService _paymentCreateService;
        private readonly ILogger<PaymentCreateEventHandler> _logger;
        public PaymentCreateEventHandler(IPaymentCreateService paymentCreateService, ILogger<PaymentCreateEventHandler> logger)
        {
            _paymentCreateService = paymentCreateService;
            _logger = logger;
        }
        public async Task HandleAsync(PaymentCreatedEvent @event, CancellationToken cancellation = default)
        {
            _logger.LogInformation("处理支付创建事件，订单号：{OrderUuid}", @event.orderMain.OrderUuid);
            // 处理支付创建事件的逻辑
            await _paymentCreateService.AddWechatPaymentAsync(
                @event.orderMain,
                @event.appId,
                @event.mchId,
                @event.openId
                );
        }
    }
}
