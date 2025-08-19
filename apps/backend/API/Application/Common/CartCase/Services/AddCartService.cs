using API.Api.Common.Models;
using API.Application.Common.CartCase.Interfaces;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate;
using API.Domain.Aggregates.CartAggregate.Interfaces;
using API.Domain.Aggregates.CartAggregates;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Application.Common.CartCase.Services
{
    public class AddCartService: IAddCartService
    {
        private readonly ICurrentService _currentService;
        private readonly ICartCreateService _cartCreateService;
        private readonly ICartReadService _cartReadService;
        private readonly ICartDomainService _cartDomainService;
        private readonly IProductReadService _productReadService;
        private readonly ILogger<AddCartService> _logger;

        public AddCartService(ICurrentService currentService, ICartCreateService cartCreateService, ICartReadService cartReadService, ICartDomainService cartDomainService, IProductReadService productReadService, ILogger<AddCartService> logger)
        {
            _currentService = currentService;
            _cartCreateService = cartCreateService;
            _cartReadService = cartReadService;
            _cartDomainService = cartDomainService;
            _productReadService = productReadService;
            _logger = logger;
        }

        public async Task<Result<CartMain>> AddCartAsync(CartWriteOptions opt)
        {
            try
            {
                var cartMainResult = await _cartDomainService.CreateCartAggregate(opt);

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
