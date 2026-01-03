using API.Api.MerchantCase.Models;
using API.Application.Common.DTOs;
using API.Application.MerchantCase.DTOs;
using API.Application.MerchantCase.Interfaces;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Services.MerchantPart.Implementations;
using API.Domain.Services.MerchantPart.Interfaces;

namespace API.Application.MerchantCase.Services
{
    public class MerchantManagementService : IMerchantManagementService
    {
        private readonly IMerchantCreateService _merchantCreateService;
        private readonly IMerchantReadService _merchantReadService;
        private readonly IMerchantUpdateService _merchantUpdateService;
        private readonly ICurrentService _currentService;
        private readonly ILogger<MerchantManagementService> _logger;

        public MerchantManagementService(
            IMerchantCreateService merchantCreateService,
            IMerchantReadService merchantReadService,
            IMerchantUpdateService merchantUpdateService,
            ICurrentService currentService,
            ILogger<MerchantManagementService> logger)
        {
            _merchantCreateService = merchantCreateService;
            _merchantReadService = merchantReadService;
            _merchantUpdateService = merchantUpdateService;
            _currentService = currentService;
            _logger = logger;
        }

        public async Task<Result<AdminMerchantResult>> CreateMerchantAsync(MerchantCreateOptions opt)
        {
            try
            {
                var adminUuid = _currentService.RequiredUuid;
                var existResult = await _merchantReadService.GetMerchantByAdminUuidAsync(adminUuid);
                if (existResult.IsSuccess)
                {
                    return Result<AdminMerchantResult>.Fail(ResultCode.InfoExist, "商户已经存在");
                }

                var createDto = new MerchantCreateDto(
                    opt.Name,
                    opt.Province,
                    opt.City,
                    opt.District,
                    opt.Detail,
                    opt.BusinessStart,
                    opt.BusinessEnd,
                    adminUuid,
                    opt.DeliveryFee,
                    opt.MinimumOrderAmount,
                    opt.FreeDeliveryThreshold
                    );

                var createResult = await _merchantCreateService.AddMerchantAsync(createDto);
                if (!createResult.IsSuccess)
                {
                    return Result<AdminMerchantResult>.Fail(createResult.Code, createResult.Message);
                }

                var merchantResult = new AdminMerchantResult
                {
                    Name = createResult.Data.Name,
                    Province = createResult.Data.Province,
                    City = createResult.Data.City,
                    District = createResult.Data.District,
                    Detail = createResult.Data.Detail,
                    BusinessStart = createResult.Data.BusinessStart,
                    BusinessEnd = createResult.Data.BusinessEnd,
                    DeliveryFee = createResult.Data.DeliveryFee,
                    MinimumOrderAmount = createResult.Data.MinimumOrderAmount,
                    FreeDeliveryThreshold = createResult.Data.FreeDeliveryThreshold,
                    IsClosed = createResult.Data.IsClosed,
                    IsAudited = createResult.Data.IsAudited
                };

                return Result<AdminMerchantResult>.Success(merchantResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建商户失败");
                return Result<AdminMerchantResult>.Fail(ResultCode.ServerError, "创建商户失败");
            }
        }

        public async Task<Result<AdminMerchantResult>> GetMerchantDetailAsync()
        {
            try
            {
                var adminUuid = _currentService.RequiredUuid;
                var readResult = await _merchantReadService.GetMerchantByAdminUuidAsync(adminUuid);
                if (!readResult.IsSuccess)
                {
                    return Result<AdminMerchantResult>.Fail(readResult.Code, readResult.Message);
                }
                var merchantResult = new AdminMerchantResult
                {
                    Name = readResult.Data.Name,
                    Province = readResult.Data.Province,
                    City = readResult.Data.City,
                    District = readResult.Data.District,
                    Detail = readResult.Data.Detail,
                    BusinessStart = readResult.Data.BusinessStart,
                    BusinessEnd = readResult.Data.BusinessEnd,
                    DeliveryFee = readResult.Data.DeliveryFee,
                    MinimumOrderAmount = readResult.Data.MinimumOrderAmount,
                    FreeDeliveryThreshold = readResult.Data.FreeDeliveryThreshold,
                    IsClosed = readResult.Data.IsClosed,
                    IsAudited = readResult.Data.IsAudited
                };
                return Result<AdminMerchantResult>.Success(merchantResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取商户信息失败");
                return Result<AdminMerchantResult>.Fail(ResultCode.ServerError, "获取商户信息失败");
            }
        }

        public async Task<Result<UserMerchantResult>> GetMerchantAsync(Guid uuid)
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
                    Detail = readResult.Data.Detail,
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

        public async Task<Result<AdminMerchantResult>> UpdateMerchantAsync(MerchantUpdateOptions opt)
        {
            try
            {
                var adminUuid = _currentService.RequiredUuid;

                var existResult = await _merchantReadService.GetMerchantByAdminUuidAsync(adminUuid);
                if (!existResult.IsSuccess)
                {
                    return Result<AdminMerchantResult>.Fail(existResult.Code, "商户不存在");
                }
                var updateDto = new MerchantUpdateDto(
                    opt.Name,
                    opt.Province,
                    opt.City,
                    opt.District,
                    opt.Detail,
                    opt.BusinessStart,
                    opt.BusinessEnd,
                    adminUuid,
                    opt.DeliveryFee,
                    opt.MinimumOrderAmount,
                    opt.FreeDeliveryThreshold
                    );
                var updateResult = await _merchantUpdateService.UpdateMerchantAsync(updateDto);
                if (!updateResult.IsSuccess)
                {
                    return Result<AdminMerchantResult>.Fail(updateResult.Code, updateResult.Message);
                }

                var merchantResult = new AdminMerchantResult
                {
                    Name = updateResult.Data.Name,
                    Province = updateResult.Data.Province,
                    City = updateResult.Data.City,
                    District = updateResult.Data.District,
                    Detail = updateResult.Data.Detail,
                    BusinessStart = updateResult.Data.BusinessStart,
                    BusinessEnd = updateResult.Data.BusinessEnd,
                    DeliveryFee = updateResult.Data.DeliveryFee,
                    MinimumOrderAmount = updateResult.Data.MinimumOrderAmount,
                    FreeDeliveryThreshold = updateResult.Data.FreeDeliveryThreshold,
                    IsClosed = updateResult.Data.IsClosed,
                    IsAudited = updateResult.Data.IsAudited
                };
                return Result<AdminMerchantResult>.Success(merchantResult);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "更新商户信息失败");
                return Result<AdminMerchantResult>.Fail(ResultCode.ServerError, "更新商户信息失败");
            }
        }
    }
}
