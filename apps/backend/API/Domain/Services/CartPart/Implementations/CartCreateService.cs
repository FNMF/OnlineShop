using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.CartPart.Interfaces;

namespace API.Domain.Services.CartPart.Implementations
{
    public class CartCreateService:ICartCreateService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartCreateService> _logger;

        public CartCreateService(ICartRepository cartRepository, ILogger<CartCreateService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<Result<Cart>> CreateCartAsync(CartCreateDto dto)
        {
            try
            {
                var result = CartFactory.Create(dto);
                if (!result.IsSuccess)
                {
                    return Result<Cart>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _cartRepository.AddCartAsync(result.Data);

                return result;
            }catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Cart>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
