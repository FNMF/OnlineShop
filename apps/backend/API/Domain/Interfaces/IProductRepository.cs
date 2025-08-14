using API.Application.Common.DTOs;
using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> QueryProducts();
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Product product);
        /*
        Task<List<Product>> GetAllProductsByMerchantUuidAsync(Guid merchantuuid);
        Task<bool> CreateProductsByMerchantUuidAsync(CUProductDto dto, Guid merchanuuid);
        Task<bool> UpdateProductsByMerchantUuidAsync(CUProductDto dto, Guid merchanuuid);
        Task<bool?> DeleteProductsByMerchantUuidAsync(Guid merchantuuid);
        */
    }
}
