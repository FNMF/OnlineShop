using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Application.ProductCase.Interfaces
{
    public interface IRemoveProductService
    {
        Task<Result<List<ProductReadDto>>> RemoveProduct(Guid uuid);
    }
}
