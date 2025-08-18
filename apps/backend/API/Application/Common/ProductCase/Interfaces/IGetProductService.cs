using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Application.Common.ProductCase.Interfaces
{
    public interface IGetProductService
    {
        Task<Result<List<ProductReadDto>>> GetAllProducts(byte[]? productUuid = null);
        Task<Result<ProductReadDetailDto>> GetProductByUuid(Guid uuid);
    }
}
