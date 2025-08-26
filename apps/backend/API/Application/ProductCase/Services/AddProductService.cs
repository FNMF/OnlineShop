using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Application.Common.EventBus;
using API.Application.ProductCase.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Enums;
using API.Domain.Events.ProductCase;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Application.ProductCase.Services
{
    public class AddProductService: IAddProductService
    {
        private readonly IProductDomainService _productDomainService;
        private readonly IProductReadService _productReadService;
        private readonly ILocalFileCreateService _localFileCreateService;
        private readonly ICurrentService _currentService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<AddProductService> _logger;

        public AddProductService(IProductDomainService productDomainService, IProductReadService productReadService, ILocalFileCreateService localFileCreateService, ICurrentService currentService, IEventBus eventBus, ILogger<AddProductService> logger)
        {
            _productDomainService = productDomainService;
            _productReadService = productReadService;
            _localFileCreateService = localFileCreateService;
            _currentService = currentService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Result<List<ProductReadDto>>> AddProduct(ProductWriteOptions opt)
        {
            try
            {
                if (opt.ProductCoverFile == null || opt.ProductCoverFile.Length == 0)
                {
                    return Result<List<ProductReadDto>>.Fail(ResultCode.InvalidInput, "商品封面不能为空");
                }
                if (opt.ProductImages == null || opt.ProductImages.Count == 0)
                {
                    return Result<List<ProductReadDto>>.Fail(ResultCode.InvalidInput, "商品图片不能为空");
                }
                var productUuid = UuidV7Helper.NewUuidV7();

                //LocalFile处理
                var imageFiles = _productDomainService.PrepareImages(productUuid, opt).Data;
                var saveResult = await _localFileCreateService.AddBatchLocalFilesAsync(imageFiles);
                if (!saveResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(saveResult.Code, saveResult.Message);
                }

                //Product处理
                var productResult = await _productDomainService.CreateProductAggregate(
                new ProductCreateDto(productUuid, opt.ProductName, opt.ProductPrice, opt.ProductStock,
                                     opt.ProductDescription, opt.ProductIngredient, opt.ProductWeight, opt.ProductIslisted,
                                     productUuid, saveResult.Data.FirstOrDefault(f => f.LocalfileObjecttype == LocalfileObjectType.product_cover.ToString()).LocalfilePath),
                imageFiles
            );

                if (!productResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(productResult.Code, productResult.Message);
                }
                //获取更新后的商品列表
                var result = await _productReadService.GetMerchantProducts();
                if (!result.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(result.Code, result.Message);
                }

                var products = result.Data.Select(p => new ProductReadDto
                    (p.ProductUuid, p.ProductName, p.ProductPrice, p.ProductStock, p.ProductWeight, p.ProductIslisted, p.ProductIsavailable, p.ProductCoverurl)).ToList();

                // 触发商品添加事件
                await _eventBus.PublishAsync(new AddProductEvent(_currentService.RequiredUuid, _currentService.CurrentType , productUuid));

                return Result<List<ProductReadDto>>.Success(products);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加商品出错");
                return Result<List<ProductReadDto>>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
