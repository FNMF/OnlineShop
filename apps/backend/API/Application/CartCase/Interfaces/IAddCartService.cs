using API.Api.Common.Models;
using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate;

namespace API.Application.CartCase.Interfaces
{
    public interface IAddCartService
    {
        Task<Result<CartMain>> AddCartAsync(CartWriteOptions opt);
    }
}
