using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.MerchantPart.Interfaces;

namespace API.Domain.Services.MerchantPart.Implementations
{
    public class MerchantCreateService:IMerchantCreateService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ILogger<MerchantCreateService> _logger;

        public MerchantCreateService(IMerchantRepository merchantRepository, ILogger<MerchantCreateService> logger)
        {
            _merchantRepository = merchantRepository;
            _logger = logger;
        }

        public async Task<Result<Merchant>> AddMerchantAsync(MerchantCreateDto dto)
        {
            try
            {
                var result = MerchantFactory.Create(dto);
                if (!result.IsSuccess)
                {
                    return Result<Merchant>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _merchantRepository.AddMerchantAsync(result.Data);

                return result;
            }catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Merchant>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
