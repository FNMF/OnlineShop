using API.Api.Common.Models;
using API.Common.Models.Results;

namespace API.Domain.Aggregates.CartAggregate.Interfaces
{
    public interface ICartDomainService
    {
        Task<Result<CartMain>> CreateCartAggregate(CartWriteOptions opt);
    }
}
