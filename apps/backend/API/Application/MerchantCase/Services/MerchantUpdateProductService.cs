using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Application.MerchantCase.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Enums;
using API.Domain.Events.MerchantCase;
using API.Domain.Services.LocalFilePart.Implementations;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Application.MerchantCase.Services
{
    public class MerchantUpdateProductService: IMerchantUpdateProductService
    {
        private readonly IProductUpdateService _productUpdateService;
        private readonly IProductReadService _productReadService;
        private readonly ILocalFileCreateService _localFileCreateService;
        private readonly ILocalFileUpdateService _localFileUpdateService;
        private readonly ILocalFileReadService _localFileReadService;
        private readonly IClientIpService _clientIpService;
        private readonly ICurrentService _currentService;
        private readonly EventBus _eventBus;
        private readonly ILogger<MerchantUpdateProductService> _logger;

        public MerchantUpdateProductService(IProductUpdateService productUpdateService, IProductReadService productReadService, ILocalFileCreateService localFileCreateService,  ILocalFileUpdateService localFileUpdateService, ILocalFileReadService localFileReadService, IClientIpService clientIpService, ICurrentService currentService, EventBus eventBus, ILogger<MerchantUpdateProductService> logger)
        {
            _productUpdateService = productUpdateService;
            _productReadService = productReadService;
            _localFileCreateService = localFileCreateService;
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

                //找到存在的封面文件
                var existingFileResult = await _localFileReadService.GetProductCoverLocalFile(uuid);

                if(!existingFileResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(existingFileResult.Code, existingFileResult.Message);
                }
                if (opt.ProductCoverFile == null || opt.ProductCoverFile.Length == 0)
                {
                    return Result<List<ProductReadDto>>.Fail(ResultCode.InvalidInput, "商品封面不能为空");
                }
                //将现有封面文件标记为已删除
                existingFileResult.Data.LocalfileIsdeleted = true;
                var pastFileUpdateResult = await _localFileUpdateService.MarkAsDeleteAsync(existingFileResult.Data);
                if (!pastFileUpdateResult.IsSuccess) 
                {
                    return Result<List<ProductReadDto>>.Fail(pastFileUpdateResult.Code, pastFileUpdateResult.Message);
                }
                // 创建新的封面文件
                var file = new LocalFileCreateDto(
                    opt.ProductCoverFile,
                    uuid.ToByteArray(), 
                    LocalfileObjectType.product_cover,
                    _currentService.CurrentUuid ,
                    ip,
                    0);     //Cover不需要SortNumer直接写0
                var fileResult = await _localFileCreateService.AddLocalFileAsync(file);
                if (!fileResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(fileResult.Code, fileResult.Message);
                }
                //详情图的处理
                if (opt.ProductImages == null || opt.ProductImages.Count == 0)
                {
                    return Result<List<ProductReadDto>>.Fail(ResultCode.InvalidInput, "商品图片不能为空");
                }
                //获取现有的详情图文件
                var pastImageFilesResult = await _localFileReadService.GetProductDetailLocalFiles(uuid);
                if(!pastImageFilesResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(pastImageFilesResult.Code, pastImageFilesResult.Message);
                }

                var pastImageFiles = pastImageFilesResult.Data;
                foreach (var pastImageFile in pastImageFiles)
                {
                    if (pastImageFile == null)
                        continue;
                    pastImageFile.LocalfileIsdeleted = true; // 标记为已删除
                }
                // 批量更新现有的详情图文件为已删除状态
                var pastImageUpdateResult = await _localFileUpdateService.BatchMarkAsDeleteAsync(pastImageFiles);
                if (!pastImageUpdateResult.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(pastImageUpdateResult.Code, pastImageUpdateResult.Message);
                }
                // 创建新的详情图文件
                var imageFiles = new List<LocalFileCreateDto>();
                foreach (var image in opt.ProductImages)
                {
                    if (image == null )
                        continue; // 跳过空文件
                    var imageFileDto = new LocalFileCreateDto(
                        image.ProductImage,
                        uuid.ToByteArray(),
                        LocalfileObjectType.product_detail, // 区分封面和详情图
                        _currentService.CurrentUuid,
                        ip,
                        image.SortNumber
                    );
                    imageFiles.Add(imageFileDto);
                }
                // 批量添加新的详情图文件
                var imageFileResults = await _localFileCreateService.AddBatchLocalFilesAsync(imageFiles);
                if (!imageFileResults.IsSuccess)
                {
                    return Result<List<ProductReadDto>>.Fail(imageFileResults.Code, imageFileResults.Message);
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
                    (p.ProductUuid, p.ProductName, p.ProductPrice, p.ProductStock, p.ProductWeight, p.ProductIslisted, p.ProductIsavailable, p.ProductCoverurl)).ToList();

                
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
