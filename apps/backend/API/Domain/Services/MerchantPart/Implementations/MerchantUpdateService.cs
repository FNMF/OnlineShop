using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.MerchantPart.Interfaces;

namespace API.Domain.Services.MerchantPart.Implementations
{
    public class MerchantUpdateService:IMerchantUpdateService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ILogger<MerchantUpdateService> _logger;

        public MerchantUpdateService(IMerchantRepository merchantRepository, ILogger<MerchantUpdateService> logger)
        {
            _merchantRepository = merchantRepository;
            _logger = logger;
        }

        public async Task<Result<Merchant>> UpdateMerchantAsync(MerchantUpdateDto dto)
        {
            try
            {
                var result = MerchantFactory.Update(dto);
                if (!result.IsSuccess)
                {
                    return Result<Merchant>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _merchantRepository.UpdateMerchantAsync(result.Data);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Merchant>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
