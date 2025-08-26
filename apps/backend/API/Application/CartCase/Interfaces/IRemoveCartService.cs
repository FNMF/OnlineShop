using API.Common.Models.Results;

namespace API.Application.CartCase.Interfaces
{
    public interface IRemoveCartService
    {
        Task<Result> RemoveCartAsync(Guid merchantUuid);
        Task<Result> RemoveCartItemAsync(Guid merchantUuid, Guid productUuid);
    }
}
