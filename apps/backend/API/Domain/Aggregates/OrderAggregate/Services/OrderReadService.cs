using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregate.Interfaces;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Aggregates.OrderAggregate.Services
{
    public class OrderReadService: IOrderReadService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderReadService> _logger;
        public OrderReadService(IOrderRepository orderRepository, ILogger<OrderReadService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Result<List<Order>>> UserGetAllOrdersByUuid(Guid uuid)
        {
            try
            {
                var orders = await _orderRepository.QueryOrders()
                    .Where(o => o.OrderUseruuid == uuid && o.OrderIsdeleted == false)
                    .ToListAsync();
                if (!orders.Any())
                {
                    _logger.LogWarning("没有找到相关订单");
                    return Result<List<Order>>.Fail(ResultCode.NotFound, "没有找到相关订单");
                }
                return Result<List<Order>>.Success(orders);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "服务器错误");
                return Result<List<Order>>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
        public async Task<Result<Order>> GetOrderByUuid(Guid uuid)
        {
            try
            {
                var order = await _orderRepository.QueryOrders()
                    .FirstOrDefaultAsync(o => o.OrderUuid == uuid && o.OrderIsdeleted == false);
                if (order == null)
                {
                    _logger.LogWarning("没有找到相关订单");
                    return Result<Order>.Fail(ResultCode.NotFound, "没有找到相关订单");
                }
                return Result<Order>.Success(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Order>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
