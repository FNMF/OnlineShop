using API.Common.Models.Results;

namespace API.Domain.Services.ProductPart.Interfaces
{
    public interface IProductRemoveService
    {
        Task<Result> RemoveProductAsync(byte[] productUuid);
    }
}
