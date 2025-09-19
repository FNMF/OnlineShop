using API.Api.MerchantCase.Models;
using API.Application.Common.DTOs;
using API.Application.Common.EventBus;
using API.Application.Interfaces;
using API.Application.MerchantCase.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Events.MerchantCase;
using API.Domain.Interfaces;
using API.Domain.Services.AdminPart.Interfaces;

namespace API.Application.MerchantCase.Services
{
    public class MerchantRegisterService:IMerchantRegisterService
    {
        private readonly EventBus _eventBus;
        private readonly IClientIpService _clientIpService;
        private readonly IShopAdminRegisterService _shopAdminRegisterService;
        private readonly ILogger<MerchantRegisterService> _logger;

        public MerchantRegisterService(EventBus eventBus, IClientIpService clientIpService, IShopAdminRegisterService shopAdminRegisterService, ILogger<MerchantRegisterService> logger)
        {
            _eventBus = eventBus;
            _clientIpService = clientIpService;
            _logger = logger;
            _shopAdminRegisterService = shopAdminRegisterService;
        }

        public async Task<Result<AdminReadDto>> RegisterByPhoneAsync(MerchantRegisterByPhoneOptions opt)
        {
            try
            {
                //test
                ValidationCodeHelper.CreateCode(opt.Phone);
                //
                var isValid = ValidationCodeHelper.ValidateCode(opt.Phone, opt.ValidationCode);
                if (!isValid.IsSuccess)
                {
                    // 2. 密码验证失败，返回错误信息
                    return Result<AdminReadDto>.Fail(isValid.Code,isValid.Message);
                }

                var dto = new ShopAdminCreateDto(opt.Phone,opt.Password,_clientIpService.GetClientIp());

                var result = _shopAdminRegisterService.Register(dto).Result;
                if (!result.IsSuccess)
                {
                    return Result<AdminReadDto>.Fail(ResultCode.InfoExist, result.Message);
                }

                await _eventBus.PublishAsync(new MerchantRegisterEvent(opt.Phone));

                var returnDto = new AdminReadDto(opt.Phone,result.Data.Key,result.Data.Uuid,result.Data.Account);

                return Result<AdminReadDto>.Success(returnDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<AdminReadDto>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
