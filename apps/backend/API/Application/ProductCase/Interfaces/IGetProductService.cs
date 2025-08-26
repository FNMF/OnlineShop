using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Application.ProductCase.Interfaces
{
    public interface IGetProductService
    {
        Task<Result<List<ProductReadDto>>> GetAllProducts(Guid? productUuid = null);
        Task<Result<ProductReadDetailDto>> GetProductByUuid(Guid uuid);
    }
}
