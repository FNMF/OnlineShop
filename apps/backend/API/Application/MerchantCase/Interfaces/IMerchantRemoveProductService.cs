using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Application.MerchantCase.Interfaces
{
    public interface IMerchantRemoveProductService
    {
        Task<Result<List<ProductReadDto>>> RemoveProduct(Guid uuid);
    }
}
