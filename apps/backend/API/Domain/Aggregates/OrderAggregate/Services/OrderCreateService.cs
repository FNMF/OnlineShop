using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregate.Interfaces;
using API.Domain.Aggregates.OrderAggregates;
using API.Domain.Interfaces;

namespace API.Domain.Aggregates.OrderAggregate.Services
{
    public class OrderCreateService: IOrderCreateService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderCreateService> _logger;

        public  OrderCreateService(IOrderRepository orderRepository, ILogger<OrderCreateService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Result<OrderMain>> CreateOrder(OrderMain orderMain)
        {
            try
            {
                var orderResult = OrderFactory.ToEntity(orderMain);
                await _orderRepository.AddOrderAsync(orderResult.Data);
                return Result<OrderMain>.Success(orderMain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建订单失败");
                return Result<OrderMain>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
