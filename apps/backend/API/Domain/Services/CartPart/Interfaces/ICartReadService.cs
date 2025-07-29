using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.CartPart.Interfaces
{
    public interface ICartReadService
    {
        Task<Result<List<Cart>>> GetAllCarts(byte[] userUuid);
    }
}
