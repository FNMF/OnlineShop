using API.Application.Common.CartCase.Interfaces;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate;
using API.Domain.Aggregates.CartAggregate.Interfaces;
using API.Domain.Aggregates.CartAggregates;

namespace API.Application.Common.CartCase.Services
{
    public class GetCartService: IGetCartService
    {
        private readonly ICurrentService _currentService;
        private readonly ICartReadService _cartReadService;
        private readonly ILogger<GetCartService> _logger;

        public GetCartService(ICurrentService currentService, ICartReadService cartReadService, ILogger<GetCartService> logger)
        {
            _currentService = currentService;
            _cartReadService = cartReadService;
            _logger = logger;
        }

        public async Task<Result<CartMain>> GetCartAsync(Guid merchantUuid)
        {
            try
            {
                var cartResult = await _cartReadService.GetCartByUuids(merchantUuid, _currentService.RequiredUuid);
                if (!cartResult.IsSuccess) 
                {
                    _logger.LogWarning("没有找到相关购物车");
                    return Result<CartMain>.Fail(cartResult.Code,cartResult.Message);
                }

                var cartMain = CartFactory.ToAggregate(cartResult.Data).Data;

                return Result<CartMain>.Success(cartMain);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "服务器错误");
                return Result<CartMain>.Fail(ResultCode.ServerError, ex.Message);
            }

        }
    }
}
