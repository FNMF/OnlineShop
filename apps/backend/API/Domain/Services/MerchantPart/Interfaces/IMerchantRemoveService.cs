using API.Common.Models.Results;

namespace API.Domain.Services.MerchantPart.Interfaces
{
    public interface IMerchantRemoveService
    {
        Task<Result> RemoveMerchantAsync(byte[] merchantUuid);
    }
}
