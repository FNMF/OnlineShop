using API.Application.Common.CartCase.Interfaces;
using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate.Interfaces;

namespace API.Application.Common.CartCase.Services
{
    public class RemoveCartService: IRemoveCartService
    {
        private readonly ICartRemoveService _cartRemoveService;
        private readonly ILogger<RemoveCartService> _logger;

        public RemoveCartService(ICartRemoveService cartRemoveService, ILogger<RemoveCartService> logger)
        {
            _cartRemoveService = cartRemoveService;
            _logger = logger;
        }

        public async Task<Result> RemoveCartAsync(Guid merchantUuid)
        {
            try
            {
                var result = await _cartRemoveService.RemoveCartAsync(merchantUuid);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("删除购物车失败: {Message}", result.Message);
                    return result;
                }
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError,ex.Message);
            }
        }
        public async Task<Result> RemoveCartItemAsync(Guid merchantUuid, Guid productUuid)
        {
            try
            {
                var result = await _cartRemoveService.RemoveCartItemAsync(merchantUuid, productUuid);
                if (!result.IsSuccess)
                {
                    _logger.LogWarning("删除购物车商品失败: {Message}", result.Message);
                    return result;
                }
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
