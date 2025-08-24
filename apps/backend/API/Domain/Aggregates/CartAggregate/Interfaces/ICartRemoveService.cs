using API.Common.Models.Results;

namespace API.Domain.Aggregates.CartAggregate.Interfaces
{
    public interface ICartRemoveService
    {
        Task<Result> RemoveCartAsync(Guid merchantUuid);
        Task<Result> RemoveCartItemAsync(Guid merchantUuid, Guid productUuid);
    }
}
