using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.MerchantPart.Interfaces;

namespace API.Domain.Services.MerchantPart.Implementations
{
    public class MerchantRemoveService:IMerchantRemoveService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ILogger<MerchantRemoveService> _logger;

        public MerchantRemoveService(IMerchantRepository merchantRepository, ILogger<MerchantRemoveService> logger)
        {
            _merchantRepository = merchantRepository;
            _logger = logger;
        }

        public async Task<Result> RemoveMerchantAsync(Guid merchantUuid)
        {
            try
            {
                if (merchantUuid == null)
                {
                    return Result.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                var query = _merchantRepository.QueryMerchants();

                var merchant = query.FirstOrDefault(a =>a.MerchantUuid == merchantUuid && a.MerchantIsdeleted == false);

                if (merchant == null)
                {
                    return Result.Fail(ResultCode.NotFound, "商户不存在或已删除");
                }

                merchant.MerchantIsdeleted = true;
                await _merchantRepository.UpdateMerchantAsync(merchant);

                return Result.Success();

            }catch (Exception ex) 
            {
                _logger.LogError(ex, "服务器错误");
                return Result.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
