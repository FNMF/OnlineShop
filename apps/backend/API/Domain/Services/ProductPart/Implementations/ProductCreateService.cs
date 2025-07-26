using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Domain.Services.ProductPart.Implementations
{
    public class ProductCreateService:IProductCreateService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductCreateService> _logger;

        public ProductCreateService(IProductRepository productRepository, ILogger<ProductCreateService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Result<Product>> AddProductAsync(ProductCreateDto dto)
        {
            try
            {
                var result = ProductFactory.Create(dto);
                if (!result.IsSuccess)
                {
                    return Result<Product>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _productRepository.AddProductAsync(result.Data);

                return result;
            }catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Product>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
