using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.PastCode
{
    public interface IMerchantUpdateProductService
    {
        Task<Result<List<ProductReadDto>>> UpdateProduct(Guid uuid, ProductWriteOptions opt);
    }
}
