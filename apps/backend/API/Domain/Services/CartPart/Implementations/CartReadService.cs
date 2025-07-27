using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.CartPart.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Services.CartPart.Implementations
{
    public class CartReadService:ICartReadService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartReadService> _logger;

        public CartReadService(ICartRepository cartRepository, ILogger<CartReadService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<Result<List<Cart>>> GetAllCarts(byte[] userUuid)
        {
            try
            {
                var query = _cartRepository.QueryCarts();

                query = query
                    .Where(u => u.CartUseruuid == userUuid);

                if (!query.Any())
                {
                    return Result<List<Cart>>.Fail(ResultCode.NotFound, "购物车不存在");
                }

                var result = await query.ToListAsync();

                return Result<List<Cart>>.Success(result);
            }catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<Cart>>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
