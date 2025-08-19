using API.Api.Common.Models;
using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate.Interfaces;
using API.Domain.Aggregates.CartAggregates;
using API.Domain.Interfaces;

namespace API.Domain.Aggregates.CartAggregate.Services
{
    public class CartCreateService: ICartCreateService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartCreateService> _cartCreateService;
        public CartCreateService(ICartRepository cartRepository, ILogger<CartCreateService> cartCreateService)
        {
            _cartRepository = cartRepository;
            _cartCreateService = cartCreateService;
        }

        public async Task<Result<CartMain>> CreateCartAsync(CartMain cartMain)
        {
            try
            {
                var cartResult = CartFactory.ToEntity(cartMain);
                if (!cartResult.IsSuccess)
                {
                    return Result<CartMain>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }
                if(!await _cartRepository.AddCartAsync(cartResult.Data))
                {
                    return Result<CartMain>.Fail(ResultCode.BusinessError, "创建购物车时出错");
                }

                return Result<CartMain>.Success(cartMain);
            }
            catch (Exception ex)
            {
                _cartCreateService.LogError(ex, "服务器错误");
                return Result<CartMain>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
