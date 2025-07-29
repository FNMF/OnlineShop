using API.Common.Models.Results;

namespace API.Domain.Services.ProductPart.Interfaces
{
    public interface IAuditRemoveService
    {
        Task<Result> RemoveProductAsync(byte[] productUuid);
    }
}
