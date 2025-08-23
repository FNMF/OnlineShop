using API.Api.Common.Models;
using API.Common.Interfaces;
using API.Common.Middlewares;
using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate.Interfaces;
using API.Domain.Aggregates.CartAggregates;
using API.Domain.Entities.Models;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Domain.Aggregates.CartAggregate.Services
{
    public class CartDomainService : ICartDomainService
    {
        private readonly ICartReadService _cartReadService;
        private readonly IProductReadService _productReadService;
        private readonly ICurrentService _currentService;
        private readonly ILogger<CartDomainService> _logger;

        public CartDomainService(ICartReadService cartReadService, IProductReadService productReadService, ICurrentService currentService, ILogger<CartDomainService> logger)
        {
            _cartReadService = cartReadService;
            _productReadService = productReadService;
            _currentService = currentService;
            _logger = logger;
        }

        public async Task<Result<CartMain>> CreateCartAggregate(CartWriteOptions opt)
        {
            try
            {
                var cartMainResult = await _cartReadService.GetCartByUuids(opt.MerchantUuid, _currentService.RequiredUuid);
                if (cartMainResult.Code == ResultCode.NotFound)
                {
                    var cartMain = new CartMain(
                    _currentService.RequiredUuid,
                    opt.MerchantUuid,
                    null
                    );
                    if (opt.Items == null || !opt.Items.Any())
                    {
                        return Result<CartMain>.Fail(ResultCode.InvalidInput, "商品输入为空");
                    }
                    foreach (var item in opt.Items)
                    {
                        var productResult = await _productReadService.GetProductByUuid(item.ProductUuid);
                        if (productResult.IsSuccess)
                        {
                            var cartItem = new CartItem(
                            item.ProductUuid,
                            productResult.Data.ProductPrice,
                            item.Quantity,
                            productResult.Data.ProductCoverurl,
                            productResult.Data.ProductName
                            );
                            cartMain.AddItem(cartItem);
                        }
                    }
                    return Result<CartMain>.Success(cartMain);
                }
                if (cartMainResult.IsSuccess)
                {
                    var cartMain = CartFactory.ToAggregate(cartMainResult.Data).Data;
                    foreach (var item in opt.Items)
                    {
                        var productResult = await _productReadService.GetProductByUuid(item.ProductUuid);
                        if (productResult.IsSuccess)
                        {
                            var cartItem = new CartItem(
                            item.ProductUuid,
                            productResult.Data.ProductPrice,
                            item.Quantity,
                            productResult.Data.ProductCoverurl,
                            productResult.Data.ProductName
                            );
                            cartMain.AddItem(cartItem);
                        }
                    }
                    return Result<CartMain>.Success(cartMain);
                }

                return Result<CartMain>.Fail(ResultCode.BusinessError, "创建Cart聚合时出错");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<CartMain>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
