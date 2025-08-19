using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Aggregates.CartAggregate.Interfaces
{
    public interface ICartReadService
    {
        Task<Result<Cart>> GetCartByUuids(byte[] merchantUuid, byte[] userUuid);
    }
}
