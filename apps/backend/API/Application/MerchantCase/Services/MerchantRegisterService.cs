using API.Api.MerchantCase.Models;
using API.Application.Common.DTOs;
using API.Application.Common.EventBus;
using API.Application.Interfaces;
using API.Application.MerchantCase.DTOs;
using API.Application.MerchantCase.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Events.MerchantCase;
using API.Domain.Interfaces;
using API.Domain.Services.AdminPart.Interfaces;
using API.Domain.Services.RefreshTokenPart.Interfaces;

namespace API.Application.MerchantCase.Services
{
    public class MerchantRegisterService:IMerchantRegisterService
    {
        private readonly EventBus _eventBus;
        private readonly IClientIpService _clientIpService;
        private readonly IShopAdminRegisterService _shopAdminRegisterService;
        private readonly IRefreshTokenCreateService _refreshTokenCreateService;
        private readonly JwtHelper _jwtHelper;
        private readonly ILogger<MerchantRegisterService> _logger;

        public MerchantRegisterService(EventBus eventBus, IClientIpService clientIpService, IShopAdminRegisterService shopAdminRegisterService, IRefreshTokenCreateService refreshTokenCreateService, JwtHelper jwtHelper, ILogger<MerchantRegisterService> logger)
        {
            _eventBus = eventBus;
            _clientIpService = clientIpService;
            _logger = logger;
            _refreshTokenCreateService = refreshTokenCreateService;
            _jwtHelper = jwtHelper;
            _shopAdminRegisterService = shopAdminRegisterService;
        }

        public async Task<Result<TokenDto>> RegisterByPhoneAsync(MerchantRegisterByPhoneOptions opt)
        {
            try
            {
                //test,todo
                ValidationCodeHelper.CreateCode(opt.Phone);
                //
                var isValid = ValidationCodeHelper.ValidateCode(opt.Phone, opt.ValidationCode);
                if (!isValid.IsSuccess)
                {
                    // 2. 密码验证失败，返回错误信息
                    return Result<TokenDto>.Fail(isValid.Code,isValid.Message);
                }

                var dto = new ShopAdminCreateDto(opt.Phone,opt.Password,_clientIpService.GetClientIp());

                var result = _shopAdminRegisterService.Register(dto).Result;
                if (!result.IsSuccess)
                {
                    return Result<TokenDto>.Fail(ResultCode.InfoExist, result.Message);
                }

                await _eventBus.PublishAsync(new MerchantRegisterEvent(opt.Phone));

                var adminJwt = _jwtHelper.AdminGenerateToken(opt.Phone, result.Data.Uuid, result.Data.Account);

                var refreshTokenResult = await _refreshTokenCreateService.AddWeekRefreshTokenAsnyc(result.Data.Uuid);
                if (!refreshTokenResult.IsSuccess)
                {
                    return Result<TokenDto>.Fail(ResultCode.ServerError, "创建刷新令牌失败");
                }

                var tokenDto = new TokenDto(adminJwt, refreshTokenResult.Data, 1200);

                return Result<TokenDto>.Success(tokenDto);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<TokenDto>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
