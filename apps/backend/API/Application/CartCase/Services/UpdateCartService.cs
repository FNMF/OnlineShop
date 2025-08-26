using API.Api.Common.Models;
using API.Application.CartCase.Interfaces;
using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate;
using API.Domain.Aggregates.CartAggregate.Interfaces;

namespace API.Application.CartCase.Services
{
    public class UpdateCartService: IUpdateCartService
    {
        private readonly ICartDomainService _cartDomainService;
        private readonly ICartUpdateService _cartUpdateService;
        private readonly ILogger<UpdateCartService> _logger;

        public UpdateCartService(ICartDomainService cartDomainService, ICartUpdateService cartUpdateService,  ILogger<UpdateCartService> logger)
        {
            _cartDomainService = cartDomainService;
            _cartUpdateService = cartUpdateService;
            _logger = logger;
        }

        public async Task<Result<CartMain>> UpdateCartAsync(CartWriteOptions opt)
        {
            try
            {
                //验证输入
                var validationResult = await _cartDomainService.ValidationInput(opt);
                if(!validationResult.IsSuccess)
                {
                    return Result<CartMain>.Fail(validationResult.Code, validationResult.Message);
                }
                
                var cartMainResult = await _cartDomainService.UpdateCartAggregate(opt);
                var updateResult = await _cartUpdateService.UpdateCartAsync(cartMainResult.Data);
                if (!updateResult.IsSuccess)
                {
                    return Result<CartMain>.Fail(updateResult.Code, updateResult.Message);
                }
                return Result<CartMain>.Success(updateResult.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<CartMain>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
