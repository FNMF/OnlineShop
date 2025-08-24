using API.Api.Common.Models;
using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate;

namespace API.Application.Common.CartCase.Interfaces
{
    public interface IUpdateCartService
    {
        Task<Result<CartMain>> UpdateCartAsync(CartWriteOptions opt);
    }
}
