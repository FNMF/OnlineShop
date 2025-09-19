using API.Application.OrderCase.Interfaces;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregate.Interfaces;
using API.Domain.Aggregates.OrderAggregates;

namespace API.Application.OrderCase.Services
{
    public class UserGetOrderService:IUserGetOrderService
    {
        private readonly IOrderReadService _orderReadService;
        private readonly ICurrentService _currentService;
        private readonly ILogger<UserGetOrderService> _logger;

        public UserGetOrderService(IOrderReadService orderReadService, ICurrentService currentService,  ILogger<UserGetOrderService> logger)
        {
            _orderReadService = orderReadService;
            _currentService = currentService;
            _logger = logger;
        }

        public async Task<Result<OrderMain>> GetOrderByUuid(Guid uuid)
        {
            try
            {
                var orderResult = await _orderReadService.GetOrderByUuid(uuid);
                if (!orderResult.IsSuccess)
                {
                    _logger.LogWarning("没有找到相关订单");
                    return Result<OrderMain>.Fail(orderResult.Code, orderResult.Message);
                }
                if(orderResult.Data.UserUuid != _currentService.RequiredUuid)
                {
                    //这里是为了匹配是否是同一个人的订单，防止越权访问
                    _logger.LogWarning("没有找到相关订单");
                    return Result<OrderMain>.Fail(ResultCode.NotFound, "没有找到相关订单");
                }

                var orderMain = OrderFactory.ToAggregate(orderResult.Data).Data;
                return Result<OrderMain>.Success(orderMain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<OrderMain>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
