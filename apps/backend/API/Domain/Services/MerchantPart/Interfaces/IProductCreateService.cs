using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.ProductPart.Interfaces
{
    public interface IProductCreateService
    {
        Task<Result<Product>> AddProductAsync(ProductCreateDto dto);
    }
}
