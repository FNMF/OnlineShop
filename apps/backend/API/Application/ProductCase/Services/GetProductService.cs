using API.Application.Common.DTOs;
using API.Application.Common.EventBus;
using API.Application.MerchantCase.Services;
using API.Application.ProductCase.Interfaces;
using API.Common.Models.Results;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Application.ProductCase.Services
{
    public class GetProductService: IGetProductService
    {
        private readonly IProductReadService _productReadService;
        private readonly ILocalFileReadService _localFileReadService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<GetProductService> _logger;

        public GetProductService(IProductReadService productReadService, ILocalFileReadService localFileReadService, IEventBus eventBus, ILogger<GetProductService> logger)
        {
            _productReadService = productReadService;
            _localFileReadService = localFileReadService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Result<List<ProductReadDto>>> GetAllProducts(Guid? productUuid = null)
        {
            try
            {
                var result = await _productReadService.GetMerchantProducts(productUuid);

                if (result.IsSuccess)
                {
                    var products = result.Data.Select(p => new ProductReadDto
                    (p.Uuid,
                    p.Name,
                    p.Price,
                    p.Stock,
                    p.Weight,
                    p.IsListed,
                    p.IsAvailable,
                    p.CoverUrl,
                    p.PackingFee)).ToList();

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

        public async Task<Result<ProductDetailReadDto>> GetProductByUuid(Guid uuid)
        {
            try
            {
                var productResult = await _productReadService.GetProductByUuid(uuid);
                var imagesResult = await _localFileReadService.GetProductDetailLocalFiles(uuid);
                if (productResult.IsSuccess)
                {
                    var product = productResult.Data;
                    var productDetail = new ProductDetailReadDto
                    (
                        product.Uuid,
                        product.Name,
                        product.Price,
                        product.Stock,
                        product.Description,
                        product.Ingredient,
                        product.Weight,
                        product.IsListed,
                        product.IsAvailable,
                        product.CoverUrl,
                        product.PackingFee,
                        imagesResult.IsSuccess ? imagesResult.Data.Select(i => i.Path).ToList() : new List<string>()
                    );
                    return Result<ProductDetailReadDto>.Success(productDetail);
                }
                else
                {
                    return Result<ProductDetailReadDto>.Fail(productResult.Code, productResult.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<ProductDetailReadDto>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
