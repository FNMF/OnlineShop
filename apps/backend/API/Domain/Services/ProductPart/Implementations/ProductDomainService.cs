using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using API.Domain.Services.LocalFilePart.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;
using static API.Api.Common.Models.ProductWriteOptions;

namespace API.Domain.Services.ProductPart.Implementations
{
    public class ProductDomainService: IProductDomainService
    {
        private readonly IProductCreateService _productCreateService;
        private readonly ILocalFileCreateService _localFileCreateService;
        private readonly ICurrentService _currentService;
        private readonly IClientIpService _clientIpService;

        public ProductDomainService(IProductCreateService productCreateService, ILocalFileCreateService localFileCreateService, ICurrentService currentService, IClientIpService clientIpService)
        {
            _productCreateService = productCreateService;
            _localFileCreateService = localFileCreateService;
            _currentService = currentService;
            _clientIpService = clientIpService;
        }

        //生成商品图片的DTO列表
        public List<LocalFileCreateDto> PrepareImages(byte[] productUuid, ProductWriteOptions opt)
        {
            if (opt.ProductCoverFile == null || opt.ProductCoverFile.Length == 0)
                throw new ArgumentException("商品封面不能为空");

            if (opt.ProductImages == null || !opt.ProductImages.Any())
                throw new ArgumentException("商品详情图不能为空");

            string ip = _clientIpService.GetClientIp();
            var currentUserId = _currentService.CurrentUuid;

            var result = new List<LocalFileCreateDto>();

            // 封面
            result.Add(new LocalFileCreateDto(
                opt.ProductCoverFile,
                productUuid,
                LocalfileObjectType.product_cover,
                currentUserId,
                ip,
                0));

            // 详情图
            foreach (var image in opt.ProductImages)
            {
                if (image == null) continue;
                result.Add(new LocalFileCreateDto(
                    image.ProductImage,
                    productUuid,
                    LocalfileObjectType.product_detail,
                    currentUserId,
                    ip,
                    image.SortNumber
                ));
            }

            return result;
        }

        //创建Product聚合
        public async Task<Result<ProductAggregateResult>> CreateProductAggregate(ProductCreateDto dto, List<LocalFileCreateDto> imageFiles)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (imageFiles == null || !imageFiles.Any())
                return Result<ProductAggregateResult>.Fail(ResultCode.InvalidInput, "图片文件不能为空");

            // 先保存图片
            var fileResult = await _localFileCreateService.AddBatchLocalFilesAsync(imageFiles);
            if (!fileResult.IsSuccess)
                return Result<ProductAggregateResult>.Fail(fileResult.Code, fileResult.Message);

            // 创建 Product
            var productResult = await _productCreateService.AddProductAsync(dto);
            if (!productResult.IsSuccess)
                return Result<ProductAggregateResult>.Fail(productResult.Code, productResult.Message);

            // 可以在这里对 Product 聚合做更多规则校验或事件触发
            // 如 productResult.Data.MarkAsAvailable() 等

            // 返回聚合信息，包括 Product 和图片路径
            return Result<ProductAggregateResult>.Success(new ProductAggregateResult
            {
                Product = productResult.Data,
                SavedFiles = fileResult.Data
            });
        }
        
       
        public class ProductAggregateResult
        {
            public Product Product { get; set; }
            public List<Localfile> SavedFiles { get; set; } // 包含封面和详情图的URL等信息
        }
    }
}
