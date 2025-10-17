using API.Application.Common.EventBus;
using API.Domain.Aggregates.OrderAggregate.Interfaces;
using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Events.PaymentCase;
using API.Domain.Interfaces;
using API.Domain.Services.PaymentPart.Interfaces;

namespace API.Application.PaymentCase.Handlers
{
    public class PaymentMarkAsPaidEventHandler : IEventHandler<PaymentMarkAsPaidEvent>
    {
        private readonly IPaymentUpdateService _paymentUpdateService;
        private readonly IOrderUpdateService _orderUpdateService;
        private readonly IOrderReadService _orderReadService;
        private readonly IUnitOfRepository _unitOfRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger<PaymentMarkAsPaidEventHandler> _logger;
        public PaymentMarkAsPaidEventHandler(
            IPaymentUpdateService paymentUpdateService,
            IOrderUpdateService orderUpdateService,
            IOrderReadService orderReadService,
            IUnitOfRepository unitOfRepository,
            IEventBus eventBus,
            ILogger<PaymentMarkAsPaidEventHandler> logger)
        {
            _paymentUpdateService = paymentUpdateService;
            _orderUpdateService = orderUpdateService;
            _orderReadService = orderReadService;
            _unitOfRepository = unitOfRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task HandleAsync(PaymentMarkAsPaidEvent @event, CancellationToken cancellation = default)
        {
            try
            {
                // 更新支付状态为已支付
                var paymentUpdateResult = await _paymentUpdateService.MarkAsAccepedNoCommit(@event.WechatTransaction);
                if (!paymentUpdateResult.IsSuccess)
                {
                    _logger.LogError("更新支付状态失败: {Message}", paymentUpdateResult.Message);
                    return;
                }
                // 获取订单信息
                var orderResult = await _orderReadService.GetOrderByUuid(Guid.Parse(@event.WechatTransaction.OutTradeNo));
                var orderMain = OrderFactory.ToAggregate(orderResult.Data).Data;
                // 更新订单状态为已支付
                orderMain.MarkAsPaid(paymentUpdateResult.Data.Uuid);
                var orderUpdateResult = _orderUpdateService.UpdateOrderNoCommit(orderMain);
                if (!orderUpdateResult.IsSuccess)
                {
                    _logger.LogError("更新订单状态失败: {Message}", orderUpdateResult.Message);
                    return;
                }
                // 提交事务
                await _unitOfRepository.CommitAsync();
                
                // TODO,发布支付已完成事件
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理支付标记为已支付事件时发生异常");
            }
        }
    }
}
