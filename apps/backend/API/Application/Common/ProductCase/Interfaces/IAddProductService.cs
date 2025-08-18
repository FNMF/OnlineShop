using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Application.Common.ProductCase.Interfaces
{
    public interface IAddProductService
    {
        Task<Result<List<ProductReadDto>>> AddProduct(ProductWriteOptions opt);
    }
}
