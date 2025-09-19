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
        private readonly ICurrentService _currentService;
        private readonly IProductRepository _ProductRepository;
        private readonly ILogger<ProductReadService> _logger;

        public ProductReadService(ICurrentService currentService, IProductRepository productRepository, ILogger<ProductReadService> logger)
        {
            _currentService = currentService;
            _ProductRepository = productRepository;
            _logger = logger;
        }
        
        public async Task<Result<List<Product>>> GetMerchantProducts(Guid? productUuid = null)
        {
            try
            {
                var merchantUuid = productUuid ?? _currentService.RequiredUuid;
                var products =await _ProductRepository.QueryProducts()
                    .Where(p => p.MerchantUuid == merchantUuid).ToListAsync();       //注意这里是对应商户的所有商品，不是不同商户的所有商品

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

        public async Task<Result<Product>> GetProductByUuid(Guid uuid)
        {
            try
            {
                var product = await _ProductRepository.QueryProducts()
                    .FirstOrDefaultAsync(p => p.Uuid == uuid);

                if (product == null)
                {
                    return Result<Product>.Fail(ResultCode.NotFound, "没有找到相关商品");
                }
                return Result<Product>.Success(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Product>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
