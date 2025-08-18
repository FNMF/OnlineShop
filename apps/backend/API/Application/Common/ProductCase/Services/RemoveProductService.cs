using API.Application.Common.DTOs;
using API.Application.Common.EventBus;
using API.Application.Common.ProductCase.Interfaces;
using API.Application.MerchantCase.Services;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Events.MerchantCase;
using API.Domain.Events.ProductCase;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Application.Common.ProductCase.Services
{
    public class RemoveProductService: IRemoveProductService
    {
        private readonly IProductRemoveService _productRemoveService;
        private readonly IProductReadService _productReadService;
        private readonly ILocalFileRemoveService _localFileRemoveService;
        private readonly ICurrentService _currentService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<RemoveProductService> _logger;

        public RemoveProductService(IProductRemoveService productRemoveService, IProductReadService productReadService, ILocalFileRemoveService localFileRemoveService, ICurrentService currentService, IEventBus eventBus, ILogger<RemoveProductService> logger)
        {
            _productRemoveService = productRemoveService;
            _productReadService = productReadService;
            _localFileRemoveService = localFileRemoveService;
            _currentService = currentService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Result<List<ProductReadDto>>> RemoveProduct(Guid uuid)
        {
            try
            {
                var productResult = await _productReadService.GetProductByUuid(uuid.ToByteArray());
                if (!productResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(productResult.Code, productResult.Message);
                }
                var fileResult = await _localFileRemoveService.RemoveProductAllLocalFilesAsync(uuid.ToByteArray());
                if (!fileResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(fileResult.Code, fileResult.Message);
                }
                var removeResult = await _productRemoveService.RemoveProductAsync(uuid.ToByteArray());
                if (!removeResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(removeResult.Code, removeResult.Message);
                }
                var result = await _productReadService.GetMerchantProducts();
                if (!result.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(result.Code, result.Message);
                }

                var products = result.Data.Select(p => new ProductReadDto
                    (p.ProductUuid, p.ProductName, p.ProductPrice, p.ProductStock, p.ProductWeight, p.ProductIslisted, p.ProductIsavailable, p.ProductCoverurl)).ToList();

                await _eventBus.PublishAsync(new RemoveProductEvent(_currentService.CurrentUuid, _currentService.CurrentType , uuid.ToByteArray()));

                return Result<List<ProductReadDto>>.Success(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除商品出错,UUID: {Uuid}", uuid);
                return Result<List<ProductReadDto>>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
