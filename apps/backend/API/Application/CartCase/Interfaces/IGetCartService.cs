using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate;

namespace API.Application.CartCase.Interfaces
{
    public interface IGetCartService
    {
        Task<Result<CartMain>> GetCartAsync(Guid merchantUuid);
    }
}
