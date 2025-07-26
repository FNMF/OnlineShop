using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.CartPart.Interfaces;

namespace API.Domain.Services.CartPart.Implementations
{
    public class CartUpdateService: ICartUpdateService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartUpdateService> _logger;

        public CartUpdateService(ICartRepository cartRepository, ILogger<CartUpdateService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<Result<Cart>> UpdateCartAsync(CartUpdateDto dto)
        {
            try
            {
                var result = CartFactory.Update(dto);
                if (!result.IsSuccess)
                {
                    return Result<Cart>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _cartRepository.UpdateCartAsync(result.Data);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Cart>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
