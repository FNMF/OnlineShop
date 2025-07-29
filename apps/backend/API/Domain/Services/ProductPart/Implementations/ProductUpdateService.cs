using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Domain.Services.ProductPart.Implementations
{
    public class ProductUpdateService:IProductUpdateService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductUpdateService> _logger;

        public ProductUpdateService(IProductRepository productRepository, ILogger<ProductUpdateService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Result<Product>> UpdateProductAsync(ProductUpdateDto dto)
        {
            try
            {
                var result = ProductFactory.Update(dto);
                if (!result.IsSuccess)
                {
                    return Result<Product>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _productRepository.UpdateProductAsync(result.Data);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Product>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
