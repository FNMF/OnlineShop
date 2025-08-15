using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Application.MerchantCase.Interfaces;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Enums;
using API.Domain.Events.MerchantCase;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Application.MerchantCase.Services
{
    public class MerchantUpdateProductService: IMerchantUpdateProductService
    {
        private readonly IProductUpdateService _productUpdateService;
        private readonly IProductReadService _productReadService;
        private readonly ILocalFileUpdateService _localFileUpdateService;
        private readonly ILocalFileReadService _localFileReadService;
        private readonly IClientIpService _clientIpService;
        private readonly ICurrentService _currentService;
        private readonly EventBus _eventBus;
        private readonly ILogger<MerchantUpdateProductService> _logger;

        public MerchantUpdateProductService(IProductUpdateService productUpdateService, IProductReadService productReadService, ILocalFileUpdateService localFileUpdateService, ILocalFileReadService localFileReadService, IClientIpService clientIpService, ICurrentService currentService, EventBus eventBus, ILogger<MerchantUpdateProductService> logger)
        {
            _productUpdateService = productUpdateService;
            _productReadService = productReadService;
            _localFileUpdateService = localFileUpdateService;
            _localFileReadService = localFileReadService;
            _clientIpService = clientIpService;
            _currentService = currentService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Result<List<ProductReadDto>>> UpdateProduct(Guid uuid, ProductWriteOptions opt)
        {
            try
            {
                string ip = _clientIpService.GetClientIp();
                var existingFileResult = await _localFileReadService.GetProductCoverLocalFile(uuid);
                var file = new LocalFileUpdateDto(opt.ProductCoverFile,_currentService.CurrentUuid ,ip,existingFileResult.Data.LocalfileUuid,uuid.ToByteArray(), LocalfileObjectType.product_cover);
                var fileResult = await _localFileUpdateService.UpdateLocalFileAsync(file);
                if (!fileResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(fileResult.Code, fileResult.Message);
                }
                var product = new ProductUpdateDto(opt.ProductName, opt.ProductPrice, opt.ProductStock, opt.ProductDescription, opt.ProductIngredient, opt.ProductWeight, opt.ProductIslisted,_currentService.CurrentUuid,uuid.ToByteArray(), fileResult.Data.LocalfilePath);
                var productResult = await _productUpdateService.UpdateProductAsync(product);
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

                
                 await _eventBus.PublishAsync(new MerchantUpdateProductEvent(_currentService.CurrentUuid, uuid.ToByteArray()));

                return Result<List<ProductReadDto>>.Success(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新商品出错");
                return Result<List<ProductReadDto>>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
