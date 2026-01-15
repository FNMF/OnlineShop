using API.Application.MerchantCase.DTOs;
using API.Application.MerchantCase.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Services.MerchantPart.Interfaces;

namespace API.Application.MerchantCase.Services
{
    public class GetMerchantService:IGetMerchantService
    {
        private readonly IMerchantReadService _merchantReadService;
        private readonly ICurrentService _currentService;
        private readonly ILogger<GetMerchantService> _logger;

        public GetMerchantService(IMerchantReadService merchantReadService, ICurrentService currentService, ILogger<GetMerchantService> logger)
        {
            _merchantReadService = merchantReadService;
            _currentService = currentService;
            _logger = logger;
        }
        public async Task<Result<UserMerchantResult>> GetMerchantAsync(Guid uuid)       // 用户获取
        {
            try
            {
                var readResult = await _merchantReadService.GetMerchantByUuidAsync(uuid);
                if (!readResult.IsSuccess)
                {
                    return Result<UserMerchantResult>.Fail(readResult.Code, readResult.Message);
                }
                var merchantResult = new UserMerchantResult
                {
                    Name = readResult.Data.Name,
                    Province = readResult.Data.Province,
                    City = readResult.Data.City,
                    District = readResult.Data.District,
                    Detail = AESHelper.Decrypt(readResult.Data.Detail),
                    BusinessStart = readResult.Data.BusinessStart,
                    BusinessEnd = readResult.Data.BusinessEnd,
                    DeliveryFee = readResult.Data.DeliveryFee,
                    MinimumOrderAmount = readResult.Data.MinimumOrderAmount,
                    FreeDeliveryThreshold = readResult.Data.FreeDeliveryThreshold,
                    IsClosed = readResult.Data.IsClosed
                };
                return Result<UserMerchantResult>.Success(merchantResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取商户信息失败");
                return Result<UserMerchantResult>.Fail(ResultCode.ServerError, "获取商户信息失败");
            }
        }

        public async Task<Result<Guid>> GetMerchantUuid()       //内部获取uuid
        {
            try
            {
                var adminUuid = _currentService.RequiredUuid;
                var readResult = await _merchantReadService.GetMerchantByAdminUuidAsync(adminUuid);
                if (!readResult.IsSuccess)
                {
                    return Result<Guid>.Fail(readResult.Code, readResult.Message);
                }
                return Result<Guid>.Success(readResult.Data.Uuid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取商户UUID失败");
                return Result<Guid>.Fail(ResultCode.ServerError, "获取商户UUID失败");
            }
        }
    }
}
