using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using static API.Api.Common.Models.ProductWriteOptions;
using static API.Domain.Services.ProductPart.Implementations.ProductDomainService;

namespace API.Domain.Services.ProductPart.Interfaces
{
    public interface IProductDomainService
    {
        Result<List<LocalFileCreateDto>> PrepareImages(Guid productUuid, ProductWriteOptions opt);
        Task<Result<ProductAggregateResult>> CreateProductAggregate(ProductCreateDto dto, List<LocalFileCreateDto> imageFiles);
        Task<Result> ValidationMerchant(Guid merchantUuid, Guid productUuid);
    }
}
