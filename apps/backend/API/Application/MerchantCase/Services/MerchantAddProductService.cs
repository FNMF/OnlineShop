using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Application.MerchantCase.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Application.MerchantCase.Services
{
    public class MerchantAddProductService: IMerchantAddProductService
    {
        private readonly IProductCreateService _productCreateService;
        private readonly IProductReadService _productReadService;
        private readonly ILocalFileCreateService _localFileCreateService;
        private readonly IClientIpService _clientIpService;
        private readonly ICurrentService _currentService;
        private readonly EventBus _eventBus;
        private readonly ILogger<MerchantAddProductService> _logger;

        public MerchantAddProductService(IProductCreateService productCreateService, IProductReadService productReadService, ILocalFileCreateService localFileCreateService, IClientIpService clientIpService, ICurrentService currentService, EventBus eventBus, ILogger<MerchantAddProductService> logger)
        {
            _productCreateService = productCreateService;
            _productReadService = productReadService;
            _localFileCreateService = localFileCreateService;
            _clientIpService = clientIpService;
            _currentService = currentService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Result<List<ProductReadDto>>> AddProduct(ProductCreateOptions opt)
        {
            try
            {
                string ip = _clientIpService.GetClientIp();
                byte[] productUuid = UuidV7Helper.NewUuidV7ToBtyes();
                var file = new LocalFileCreateDto(opt.ProductCoverFile, productUuid, LocalfileObjectType.product, _currentService.CurrentUuid, ip);
                var fileResult = await _localFileCreateService.AddLocalFileAsync(file);

                if (!fileResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(fileResult.Code, fileResult.Message);
                }

                var product = new ProductCreateDto(productUuid, opt.ProductName, opt.ProductPrice, opt.ProductStock, opt.ProductDescription, opt.ProductIngredient, opt.ProductWeight, opt.ProductIslisted, productUuid, fileResult.Data.LocalfilePath);
                var productResult = await _productCreateService.AddProductAsync(product);

                if (!productResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(productResult.Code, productResult.Message);
                }

                var result = await _productReadService.GetMerchantProducts();
                if (!result.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(result.Code, result.Message);
                }

                var products = result.Data.Select(p => new ProductReadDto
                    (p.ProductUuid, p.ProductName, p.ProductPrice, p.ProductStock, p.ProductDescription,
                    p.ProductIngredient, p.ProductWeight, p.ProductIslisted, p.ProductIsavailable, p.ProductCoverurl)).ToList();

                return Result<List<ProductReadDto>>.Success(products);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<List<ProductReadDto>>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
