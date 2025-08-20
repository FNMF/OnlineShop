using API.Common.Models.Results;

namespace API.Application.Common.CartCase.Interfaces
{
    public interface IRemoveCartService
    {
        Task<Result> RemoveCart(Guid merchantUuid);
    }
}
