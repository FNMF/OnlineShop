using API.Application.Common.DTOs;
using API.Application.MerchantCase.Interfaces;
using API.Common.Models.Results;
using API.Domain.Services.ProductPart.Interfaces;
using System.Runtime.InteropServices;

namespace API.Application.MerchantCase.Services
{
    public class MerchantGetProductsService:IMerchantGetProductsService
    {
        private readonly IProductReadService _productReadService;
        private readonly EventBus _eventBus;
        private readonly ILogger<MerchantGetProductsService> _logger;

        public MerchantGetProductsService(IProductReadService productReadService, EventBus eventBus, ILogger<MerchantGetProductsService> logger)
        {
            _productReadService = productReadService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Result<List<ProductReadDto>>> GetAllProducts()
        {
            try
            {
                var result = await _productReadService.GetMerchantProducts();

                if (result.IsSuccess)
                {
                    var productDtos = result.Data.Select(p => new ProductReadDto
                    (p.ProductUuid, p.ProductName, p.ProductPrice, p.ProductStock, p.ProductDescription, 
                    p.ProductIngredient, p.ProductWeight, p.ProductIslisted, p.ProductIsavailable, p.ProductCoverurl)).ToList();

                    return Result<List<ProductReadDto>>.Success(productDtos);
                }
                else
                {
                    return Result<List<ProductReadDto>>.Fail(result.Code, result.Message);
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "服务器错误");
                return Result<List<ProductReadDto>>.Fail(ResultCode.ServerError, ex.Message);
            }

        }

    }
}
