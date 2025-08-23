using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.ProductPart.Interfaces
{
    public interface IProductReadService
    {
        Task<Result<List<Product>>> GetMerchantProducts(Guid? productUuid = null);
        Task<Result<Product>> GetProductByUuid(Guid uuidBytes);
    }
}
