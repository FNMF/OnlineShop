using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.PastCode
{
    public interface IMerchantAddProductService
    {
        Task<Result<List<ProductReadDto>>> AddProduct(ProductWriteOptions opt);
    }
}
