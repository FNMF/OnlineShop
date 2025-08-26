using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Application.Common.EventBus;
using API.Application.MerchantCase.Services;
using API.Application.ProductCase.Interfaces;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using API.Domain.Events.MerchantCase;
using API.Domain.Events.ProductCase;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Application.ProductCase.Services
{
    public class UpdateProductService: IUpdateProductService
    {
        private readonly IProductDomainService _productDomainService;
        private readonly IProductUpdateService _productUpdateService;
        private readonly IProductReadService _productReadService;
        private readonly ILocalFileCreateService _localFileCreateService;
        private readonly ILocalFileUpdateService _localFileUpdateService;
        private readonly ILocalFileReadService _localFileReadService;
        private readonly ICurrentService _currentService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<UpdateProductService> _logger;

        public UpdateProductService(IProductDomainService productDomainService, IProductUpdateService productUpdateService, IProductReadService productReadService, ILocalFileCreateService localFileCreateService, ILocalFileUpdateService localFileUpdateService, ILocalFileReadService localFileReadService, ICurrentService currentService, IEventBus eventBus, ILogger<UpdateProductService> logger)
        {
            _productDomainService = productDomainService;
            _productUpdateService = productUpdateService;
            _productReadService = productReadService;
            _localFileCreateService = localFileCreateService;
            _localFileUpdateService = localFileUpdateService;
            _localFileReadService = localFileReadService;
            _currentService = currentService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Result<List<ProductReadDto>>> UpdateProduct(Guid uuid, ProductWriteOptions opt)
        {
            try
            {
                // 获取现有封面和详情图
                var existingCoverResult = await _localFileReadService.GetProductCoverLocalFile(uuid);
                if (!existingCoverResult.IsSuccess)
                    return Result<List<ProductReadDto>>.Fail(existingCoverResult.Code, existingCoverResult.Message);

                var existingDetailResult = await _localFileReadService.GetProductDetailLocalFiles(uuid);
                if (!existingDetailResult.IsSuccess)
                    return Result<List<ProductReadDto>>.Fail(existingDetailResult.Code, existingDetailResult.Message);

                // 标记旧文件为删除
                var allOldFiles = new List<Localfile> { existingCoverResult.Data };
                allOldFiles.AddRange(existingDetailResult.Data);

                var markDeleteResult = await _localFileUpdateService.BatchMarkAsDeleteAsync(allOldFiles);
                if (!markDeleteResult.IsSuccess)
                    return Result<List<ProductReadDto>>.Fail(markDeleteResult.Code, markDeleteResult.Message);

                // 构造新的封面+详情图 DTO
                var newFiles = _productDomainService.PrepareImages(uuid, opt).Data;

                // 上传新的文件
                var uploadResult = await _localFileCreateService.AddBatchLocalFilesAsync(newFiles);
                if (!uploadResult.IsSuccess)
                    return Result<List<ProductReadDto>>.Fail(uploadResult.Code, uploadResult.Message);

                // 更新 Product 聚合
                var productDto = new ProductUpdateDto(
                    opt.ProductName,
                    opt.ProductPrice,
                    opt.ProductStock,
                    opt.ProductDescription,
                    opt.ProductIngredient,
                    opt.ProductWeight,
                    opt.ProductIslisted,
                    _currentService.RequiredUuid,
                    uuid,
                    uploadResult.Data.First(f => f.LocalfileObjecttype == LocalfileObjectType.product_cover.ToString()).LocalfilePath
                );

                var productResult = await _productUpdateService.UpdateProductAsync(productDto);
                if (!productResult.IsSuccess)
                    return Result<List<ProductReadDto>>.Fail(productResult.Code, productResult.Message);

                // 读取更新后的商品列表
                var result = await _productReadService.GetMerchantProducts();
                if (!result.IsSuccess)
                    return Result<List<ProductReadDto>>.Fail(result.Code, result.Message);

                var products = result.Data.Select(p => new ProductReadDto(
                    p.ProductUuid,
                    p.ProductName,
                    p.ProductPrice,
                p.ProductStock,
                p.ProductWeight,
                p.ProductIslisted,
                    p.ProductIsavailable,
                    p.ProductCoverurl
                )).ToList();

                await _eventBus.PublishAsync(new UpdateProductEvent(_currentService.RequiredUuid, _currentService.CurrentType , uuid));

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
