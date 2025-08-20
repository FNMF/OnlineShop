using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate.Interfaces;
using API.Domain.Aggregates.CartAggregates;
using API.Domain.Interfaces;

namespace API.Domain.Aggregates.CartAggregate.Services
{
    public class CartRemoveService: ICartRemoveService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICurrentService _currentService;
        private readonly ILogger<CartRemoveService> _logger;
        public CartRemoveService(ICartRepository cartRepository,ICurrentService currentService,  ILogger<CartRemoveService> logger)
        {
            _cartRepository = cartRepository;
            _currentService = currentService;
            _logger = logger;
        }
        public async Task<Result> RemoveCartAsync(Guid merchantUuid)
        {
            try
            {
                var cart = _cartRepository.QueryCarts().FirstOrDefault(c => c.CartMerchantuuid == merchantUuid.ToByteArray()&&c.CartUseruuid == _currentService.CurrentUuid &&c.CartIsdeleted == false);
                if (cart == null)
                {
                    _logger.LogWarning("没有找到相关购物车");
                    return Result.Fail(ResultCode.NotFound, "没有找到相关购物车");
                }
                cart.CartIsdeleted = true; // 标记为已删除
                if (!await _cartRepository.UpdateCartAsync(cart))
                {
                    return Result.Fail(ResultCode.BusinessError, "删除购物车时出错");
                }
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
