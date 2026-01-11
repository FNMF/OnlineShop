using API.Application.MerchantCase.Interfaces;
using API.Application.OrderCase.Interfaces;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Aggregates.OrderAggregate.Interfaces;
using API.Domain.Aggregates.OrderAggregates;

namespace API.Application.OrderCase.Services
{
    public class MerchantGetAllOrdersService: IMerchantGetAllOrdersService
    {
        private readonly IOrderReadService _orderReadService;
        private readonly IGetMerchantService _getMerchantService;
        private readonly ICurrentService _currentService;
        private readonly ILogger<MerchantGetAllOrdersService> _logger;

        public MerchantGetAllOrdersService(IOrderReadService orderReadService, IGetMerchantService getMerchantService, ICurrentService currentService, ILogger<MerchantGetAllOrdersService> logger)
        {
            _orderReadService = orderReadService;
            _getMerchantService = getMerchantService;
            _currentService = currentService;
            _logger = logger;
        }

        public async Task<Result<List<OrderMain>>> GetAllOrders()
        {
            try
            {
                var merchantResult = await _getMerchantService.GetMerchantUuid();
                if (!merchantResult.IsSuccess)
                {
                    return Result<List<OrderMain>>.Fail(merchantResult.Code, merchantResult.Message);
                }
                var merchantUuid = merchantResult.Data;
                var ordersResult = await _orderReadService.MerchantGetAllOrdersByUuid(merchantUuid);
                if (!ordersResult.IsSuccess)
                {
                    return Result<List<OrderMain>>.Fail(ordersResult.Code, ordersResult.Message);
                }
                var orderMains = new List<OrderMain>();
                foreach (var order in ordersResult.Data)
                {
                    var orderResult = OrderFactory.ToAggregate(order);
                    if (orderResult.IsSuccess)      //这里选择跳过脏数据
                    {
                        orderMains.Add(orderResult.Data);
                    }
                }

                return Result<List<OrderMain>>.Success(orderMains);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<List<OrderMain>>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
