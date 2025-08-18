using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.PastCode
{
    public interface IMerchantRemoveProductService
    {
        Task<Result<List<ProductReadDto>>> RemoveProduct(Guid uuid);
    }
}
