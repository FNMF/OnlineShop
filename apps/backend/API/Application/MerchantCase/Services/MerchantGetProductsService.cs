using API.Application.Common.DTOs;
using API.Application.MerchantCase.Interfaces;
using API.Common.Models.Results;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;
using System.Runtime.InteropServices;

namespace API.Application.MerchantCase.Services
{
    public class MerchantGetProductsService:IMerchantGetProductsService
    {
        private readonly IProductReadService _productReadService;
        private readonly ILocalFileReadService _localFileReadService;
        private readonly EventBus _eventBus;
        private readonly ILogger<MerchantGetProductsService> _logger;

        public MerchantGetProductsService(IProductReadService productReadService, ILocalFileReadService localFileReadService, EventBus eventBus, ILogger<MerchantGetProductsService> logger)
        {
            _productReadService = productReadService;
            _localFileReadService = localFileReadService;
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
                    var products = result.Data.Select(p => new ProductReadDto
                    (p.ProductUuid, 
                    p.ProductName, 
                    p.ProductPrice, 
                    p.ProductStock, 
                    p.ProductWeight, 
                    p.ProductIslisted, 
                    p.ProductIsavailable, 
                    p.ProductCoverurl)).ToList();

                    return Result<List<ProductReadDto>>.Success(products);
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

        public async Task<Result<ProductReadDetailDto>> GetProductByUuid(Guid uuid)
        {
            try
            {
                var uuidBytes = uuid.ToByteArray();
                var productResult = await _productReadService.GetProductByUuid(uuidBytes);
                var imagesResult = await _localFileReadService.GetProductDetailLocalFiles(uuid);
                if (productResult.IsSuccess)
                {
                    var product = productResult.Data;
                    var productDetail = new ProductReadDetailDto
                    (
                        product.ProductUuid,
                        product.ProductName,
                        product.ProductPrice,
                        product.ProductStock,
                        product.ProductDescription,
                        product.ProductIngredient,
                        product.ProductWeight,
                        product.ProductIslisted,
                        product.ProductIsavailable,
                        product.ProductCoverurl,
                        imagesResult.IsSuccess ? imagesResult.Data.Select(i => i.LocalfilePath).ToList() : new List<string>()
                    );
                    return Result<ProductReadDetailDto>.Success(productDetail);
                }
                else
                {
                    return Result<ProductReadDetailDto>.Fail(productResult.Code, productResult.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<ProductReadDetailDto>.Fail(ResultCode.ServerError, ex.Message);
            }
        }

    }
}
