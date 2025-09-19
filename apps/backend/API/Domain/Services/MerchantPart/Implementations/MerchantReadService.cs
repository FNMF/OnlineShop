using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.MerchantPart.Interfaces;

namespace API.Domain.Services.MerchantPart.Implementations
{
    public class MerchantReadService: IMerchantReadService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ILogger<MerchantReadService> _logger;
        public MerchantReadService(IMerchantRepository merchantRepository, ILogger<MerchantReadService> logger)
        {
            _merchantRepository = merchantRepository;
            _logger = logger;
        }

        public async Task<Result<Merchant>> GetMerchantByUuidAsync(Guid uuid) 
        {
            try 
            {
                var merchant = _merchantRepository.QueryMerchants()
                    .FirstOrDefault(m => m.Uuid == uuid && m.IsDeleted == false);
                if (merchant == null)
                {
                    return Result<Merchant>.Fail(ResultCode.NotFound, "没有找到相关商户");
                }

                return Result<Merchant>.Success(merchant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<Merchant>.Fail(ResultCode.ServerError, "查询商户时出错");
            }
            
        }
    }
}
