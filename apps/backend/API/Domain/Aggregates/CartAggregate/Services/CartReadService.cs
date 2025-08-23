using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate.Interfaces;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Aggregates.CartAggregate.Services
{
    public class CartReadService:ICartReadService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartReadService> _logger;

        public CartReadService(ICartRepository cartRepository, ILogger<CartReadService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<Result<Cart>> GetCartByUuids(Guid merchantUuid, Guid userUuid)
        {
            try
            {
                var cart = await _cartRepository.QueryCarts()
                    .FirstOrDefaultAsync(c => c.CartMerchantuuid == merchantUuid && c.CartUseruuid == userUuid &&c.CartIsdeleted == false );

                if (cart == null)
                {
                    _logger.LogWarning("没有找到相关购物车");
                    return Result<Cart>.Fail(ResultCode.NotFound, "没有找到相关购物车");
                }
                return Result<Cart>.Success(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Cart>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
