using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Application.MerchantCase.Interfaces
{
    public interface IMerchantGetProductsService
    {
        Task<Result<List<ProductReadDto>>> GetAllProducts();
        Task<Result<ProductReadDetailDto>> GetProductByUuid(Guid uuid);
    }
}
