using API.Entities.Dto;
using API.Entities.Models;

namespace API.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsByMerchantUuidAsync(Guid merchantuuid);
        Task<bool> CreateProductsByMerchantUuidAsync(CUProductDto dto ,Guid merchanuuid);
        Task<bool> UpdateProductsByMerchantUuidAsync(CUProductDto dto, Guid merchanuuid);
        Task<bool?> DeleteProductsByMerchantUuidAsync(Guid merchantuuid);

    }
}
