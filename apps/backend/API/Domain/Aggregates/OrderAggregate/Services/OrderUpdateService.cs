using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregate.Interfaces;
using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Interfaces;

namespace API.Domain.Aggregates.OrderAggregate.Services
{
    public class OrderUpdateService: IOrderUpdateService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderUpdateService> _logger;
        public OrderUpdateService(IOrderRepository orderRepository, ILogger<OrderUpdateService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<Result<OrderMain>> UpdateOrder(OrderMain orderMain)
        {
            try
            {
                var orderResult = OrderFactory.ToEntity(orderMain);
                if (!orderResult.IsSuccess)
                {
                    return Result<OrderMain>.Fail(orderResult.Code, orderResult.Message);
                }
                var updateResult = await _orderRepository.UpdateOrderAsync(orderResult.Data);
                if (!updateResult)
                {
                    return Result<OrderMain>.Fail(ResultCode.ServerError, "更新订单失败");
                }
                return Result<OrderMain>.Success(orderMain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新订单失败");
                return Result<OrderMain>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
        public Result<OrderMain> UpdateOrderNoCommit(OrderMain orderMain)
        {
            try
            {
                var orderResult = OrderFactory.ToEntity(orderMain);
                if (!orderResult.IsSuccess)
                {
                    return Result<OrderMain>.Fail(orderResult.Code, orderResult.Message);
                }
                var updateResult = _orderRepository.UpdateOrderAsyncNoCommit(orderResult.Data);
                if (!updateResult)
                {
                    return Result<OrderMain>.Fail(ResultCode.ServerError, "更新订单失败");
                }
                return Result<OrderMain>.Success(orderMain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新订单失败");
                return Result<OrderMain>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
