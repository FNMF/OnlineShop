using API.Common.Models.Results;

namespace API.Domain.Services.AddressPart.Interfaces
{
    public interface IAddressRemoveService
    {
        Task<Result> RemoveAddressAsync(Guid addressUuid);
    }
}
