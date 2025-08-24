using API.Common.Models.Results;

namespace API.Domain.Aggregates.CartAggregate.Interfaces
{
    public interface ICartUpdateService
    {
        Task<Result<CartMain>> UpdateCartAsync(CartMain cartMain);
    }
}
