using API.Common.Models.Results;

namespace API.PastCode.CartPart.Interfaces
{
    public interface ICartRemoveService
    {
        Task<Result> RemoveCartAsync(byte[] cartUuid);
    }
}
