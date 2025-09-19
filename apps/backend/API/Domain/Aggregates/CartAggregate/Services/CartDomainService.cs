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
        private readonly IProductDomainService _productDomainService;
        private readonly ICurrentService _currentService;
        private readonly ILogger<CartDomainService> _logger;

        public CartDomainService(ICartReadService cartReadService, IProductReadService productReadService, IProductDomainService productDomainService, ICurrentService currentService, ILogger<CartDomainService> logger)
        {
            _cartReadService = cartReadService;
            _productReadService = productReadService;
            _productDomainService = productDomainService;
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
                            productResult.Data.Price,
                            productResult.Data.PackingFee,
                            item.Quantity,
                            productResult.Data.CoverUrl,
                            productResult.Data.Name
                            );
                            cartMain.UpdateItem(cartItem);
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
                            productResult.Data.Price,
                            productResult.Data.PackingFee,
                            item.Quantity,
                            productResult.Data.CoverUrl,
                            productResult.Data.Name
                            );
                            cartMain.UpdateItem(cartItem);
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
        public async Task<Result<CartMain>> UpdateCartAggregate(CartWriteOptions opt)
        {
            try
            {
                var cartMainResult = await _cartReadService.GetCartByUuids(opt.MerchantUuid, _currentService.RequiredUuid);
                if (!cartMainResult.IsSuccess)
                {
                    return Result<CartMain>.Fail(cartMainResult.Code, cartMainResult.Message);
                }
                var cartMain = CartFactory.ToAggregate(cartMainResult.Data).Data;
                foreach (var item in opt.Items)
                {
                    var productResult = await _productReadService.GetProductByUuid(item.ProductUuid);
                    if (productResult.IsSuccess)
                    {
                        var cartItem = new CartItem(
                            item.ProductUuid,
                            productResult.Data.Price,
                            productResult.Data.PackingFee,
                            item.Quantity,
                            productResult.Data.CoverUrl,
                            productResult.Data.Name
                        );
                        cartMain.UpdateItem(cartItem);
                    }
                }
                return Result<CartMain>.Success(cartMain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<CartMain>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
        public async Task<Result> ValidationInput(CartWriteOptions opt)
        {
            if (opt == null)
            {
                return Result.Fail(ResultCode.InvalidInput, "输入不能为空");
            }
            if (opt.MerchantUuid == Guid.Empty)
            {
                return Result.Fail(ResultCode.InvalidInput, "商户ID不能为空");
            }
            if (_currentService.RequiredUuid == Guid.Empty)
            {
                return Result.Fail(ResultCode.InvalidInput, "用户ID不能为空");
            }
            if (opt.Items == null || !opt.Items.Any())
            {
                return Result.Fail(ResultCode.InvalidInput, "购物车不能为空");
            }
            var result = await _productDomainService.ValidationMerchant(opt.MerchantUuid, opt.Items.FirstOrDefault().ProductUuid);
            if (!result.IsSuccess)
                return Result.Fail(ResultCode.InvalidInput, "商品不属于该商户");
            return Result.Success();
        }
        public async Task<Result> ValidationRole(Guid userUuid, Guid cartUuid)
        {
            if (userUuid == Guid.Empty)
            {
                return Result.Fail(ResultCode.InvalidInput, "用户ID不能为空");
            }
            if (cartUuid == Guid.Empty)
            {
                return Result.Fail(ResultCode.InvalidInput, "购物车ID不能为空");
            }
            if (_cartReadService.GetCartByUuid(cartUuid).Result.Data.UserUuid != userUuid)
            {
                return Result.Fail(ResultCode.Forbidden, "没有权限操作该购物车");
            }

            return Result.Success();
        }
    }
}
