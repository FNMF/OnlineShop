using API.Api.Common.Models;
using API.Application.Common.CartCase.Interfaces;
using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate;
using API.Domain.Aggregates.CartAggregate.Interfaces;

namespace API.Application.Common.CartCase.Services
{
    public class AddCartService: IAddCartService
    {
        private readonly ICartCreateService _cartCreateService;
        private readonly ICartDomainService _cartDomainService;
        private readonly ILogger<AddCartService> _logger;

        public AddCartService( ICartCreateService cartCreateService, ICartDomainService cartDomainService, ILogger<AddCartService> logger)
        {
            _cartCreateService = cartCreateService;
            _cartDomainService = cartDomainService;
            _logger = logger;
        }

        public async Task<Result<CartMain>> AddCartAsync(CartWriteOptions opt)
        {
            try
            {
                var cartMainResult = await _cartDomainService.CreateCartAggregate(opt);
                if (!cartMainResult.IsSuccess)
                {
                    return Result<CartMain>.Fail(cartMainResult.Code, cartMainResult.Message);
                }

                var result = await _cartCreateService.CreateCartAsync(cartMainResult.Data);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("添加购物车失败: {Message}", result.Message);
                    return Result<CartMain>.Fail(result.Code, result.Message);
                }
                return Result<CartMain>.Success(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<CartMain>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
