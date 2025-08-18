using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Application.MerchantCase.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Enums;
using API.Domain.Events.MerchantCase;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Application.MerchantCase.Services
{
    public class MerchantAddProductService : IMerchantAddProductService
    {
        private readonly IProductDomainService _productDomainService;
        private readonly IProductReadService _productReadService;
        private readonly ILocalFileCreateService _localFileCreateService;
        private readonly ICurrentService _currentService;
        private readonly EventBus _eventBus;
        private readonly ILogger<MerchantAddProductService> _logger;

        public MerchantAddProductService(IProductDomainService productDomainService, IProductReadService productReadService, ILocalFileCreateService localFileCreateService,ICurrentService currentService , EventBus eventBus, ILogger<MerchantAddProductService> logger)
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
                byte[] productUuid = UuidV7Helper.NewUuidV7ToBtyes();

                //LocalFile处理
                var imageFiles = _productDomainService.PrepareImages(productUuid,opt);
                var saveResult = await _localFileCreateService.AddBatchLocalFilesAsync(imageFiles);
                if (!saveResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(saveResult.Code, saveResult.Message);
                }

                //Product处理
                var productResult = await _productDomainService.CreateProductAggregate(
                new ProductCreateDto(productUuid, opt.ProductName, opt.ProductPrice, opt.ProductStock,
                                     opt.ProductDescription, opt.ProductIngredient, opt.ProductWeight, opt.ProductIslisted,
                                     productUuid, saveResult.Data.FirstOrDefault(f =>f.LocalfileObjecttype == LocalfileObjectType.product_cover.ToString()).LocalfilePath),
                imageFiles
            );

                if (!productResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(productResult.Code, productResult.Message);
                }

                /* 这是旧代码
                var file = new LocalFileCreateDto(opt.ProductCoverFile, productUuid, LocalfileObjectType.product_cover, _currentService.CurrentUuid, ip, 0);
                var fileResult = await _localFileCreateService.AddLocalFileAsync(file);

                if (!fileResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(fileResult.Code, fileResult.Message);
                }

                var imagefiles = new List<LocalFileCreateDto>();

                foreach (var image in opt.ProductImages)
                {
                    if (image == null)
                        continue; // 跳过空文件

                    var imageFileDto = new LocalFileCreateDto(
                        image.ProductImage,
                        productUuid,
                        LocalfileObjectType.product_detail, // 区分封面和详情图
                        _currentService.CurrentUuid,
                        ip,
                        image.SortNumber
                    );

                    imagefiles.Add(imageFileDto);
                }
                var imageFileResults = await _localFileCreateService.AddBatchLocalFilesAsync(imagefiles);
                if (!imageFileResults.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(imageFileResults.Code, imageFileResults.Message);
                }

                var product = new ProductCreateDto(productUuid, opt.ProductName, opt.ProductPrice, opt.ProductStock, opt.ProductDescription, opt.ProductIngredient, opt.ProductWeight, opt.ProductIslisted, productUuid, fileResult.Data.LocalfilePath);
                var productResult = await _productCreateService.AddProductAsync(product);

                if (!productResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(productResult.Code, productResult.Message);
                }*/
                //获取更新后的商品列表
                var result = await _productReadService.GetMerchantProducts();
                if (!result.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(result.Code, result.Message);
                }

                var products = result.Data.Select(p => new ProductReadDto
                    (p.ProductUuid, p.ProductName, p.ProductPrice, p.ProductStock, p.ProductWeight, p.ProductIslisted, p.ProductIsavailable, p.ProductCoverurl)).ToList();

                // 触发商品添加事件
                await _eventBus.PublishAsync(new MerchantAddProductEvent(_currentService.CurrentUuid, productUuid));

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
