using API.Common.Models.Results;
using API.Domain.Interfaces;
using API.Domain.Services.CartPart.Interfaces;

namespace API.Domain.Services.CartPart.Implementations
{
    public class CartRemoveService: ICartRemoveService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartRemoveService> _logger;    

        public CartRemoveService(ICartRepository cartRepository, ILogger<CartRemoveService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<Result> RemoveCartAsync(byte[] cartUuid)
        {
            try
            {
                if(cartUuid == null)
                {
                    return Result.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                var query = _cartRepository.QueryCarts();

                var cart = query.FirstOrDefault(a =>a.CartUuid == cartUuid);

                if(cart == null)
                {
                    return Result.Fail(ResultCode.NotFound, "购物车不存在或已删除");
                }

                await _cartRepository.RemoveCartAsync(cart);

                return Result.Success();
            }catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
