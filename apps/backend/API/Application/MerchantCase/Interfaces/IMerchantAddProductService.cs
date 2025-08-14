using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Application.MerchantCase.Interfaces
{
    public interface IMerchantAddProductService
    {
        Task<Result<List<ProductReadDto>>> AddProduct(ProductCreateOptions opt);
    }
}
