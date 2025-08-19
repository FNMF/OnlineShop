using API.Common.Models.Results;

namespace API.Domain.Aggregates.CartAggregate.Interfaces
{
    public interface ICartCreateService
    {
        Task<Result<CartMain>> CreateCartAsync(CartMain cartMain);
    }
}
