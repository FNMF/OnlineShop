using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate.Interfaces;
using API.Domain.Aggregates.CartAggregates;
using API.Domain.Interfaces;

namespace API.Domain.Aggregates.CartAggregate.Services
{
    public class CartUpdateService: ICartUpdateService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartUpdateService> _logger;

        public CartUpdateService(ICartRepository cartRepository, ILogger<CartUpdateService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<Result<CartMain>> UpdateCartAsync(CartMain cartMain)
        {
            try
            {
                var cartResult = CartFactory.ToEntity(cartMain);
                if (!cartResult.IsSuccess)
                {
                    return Result<CartMain>.Fail(ResultCode.InvalidInput, "输入数据不合法");
                }
                if (!await _cartRepository.UpdateCartAsync(cartResult.Data))
                {
                    return Result<CartMain>.Fail(ResultCode.BusinessError, "更新购物车时出错");
                }
                return Result<CartMain>.Success(cartMain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<CartMain>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
