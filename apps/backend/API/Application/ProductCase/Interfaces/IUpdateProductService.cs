using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;

namespace API.Application.ProductCase.Interfaces
{
    public interface IUpdateProductService
    {
        Task<Result<List<ProductReadDto>>> UpdateProduct(Guid uuid, ProductWriteOptions opt);
    }
}
