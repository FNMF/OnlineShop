using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Services.ProductPart.Implementations
{
    public class ProductReadService:IProductReadService
    {
        public readonly ICurrentService _currentService;
        public readonly IProductRepository _ProductRepository;
        public readonly ILogger<ProductReadService> _logger;

        public ProductReadService(ICurrentService currentService, IProductRepository productRepository, ILogger<ProductReadService> logger)
        {
            _currentService = currentService;
            _ProductRepository = productRepository;
            _logger = logger;
        }
        
        public async Task<Result<List<Product>>> GetMerchantProducts()
        {
            try
            {
                var merchantUuid = _currentService.CurrentUuid;
                var products =await _ProductRepository.QueryProducts()
                    .Where(p => p.ProductMerchantuuid == merchantUuid).ToListAsync();

                if (products == null || products.Count == 0)
                {
                    return Result<List<Product>>.Fail(ResultCode.NotFound, "没有找到相关商品");
                }

                return Result<List<Product>>.Success(products);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "服务器错误");
                return Result<List<Product>>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
