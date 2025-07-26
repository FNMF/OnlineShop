using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Domain.Services.ProductPart.Implementations
{
    public class ProductRemoveService:IProductRemoveService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductRemoveService> _logger;

        public ProductRemoveService(IProductRepository productRepository, ILogger<ProductRemoveService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Result> RemoveProductAsync(byte[] productUuid)
        {
            try
            {
                if (productUuid == null)
                {
                    return Result.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                var query = _productRepository.QueryProducts();

                var product = query.FirstOrDefault(a =>a.ProductUuid == productUuid && a.ProductIsdeleted == false);

                if (product == null)
                {
                    return Result.Fail(ResultCode.NotFound, "商品不存在或已删除");
                }

                product.ProductIsdeleted = true;
                await _productRepository.UpdateProductAsync(product);

                return Result.Success();

            }catch (Exception ex) 
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
