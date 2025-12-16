using API.Api.IdentityCase.Models;
using API.Application.Common.DTOs;
using API.Application.Common.EventBus;
using API.Application.IdentityCase.DTOs;
using API.Application.IdentityCase.Interfaces;
using API.Application.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Events.MerchantCase;
using API.Domain.Interfaces;
using API.Domain.Services.IdentityPart.Interfaces;
using API.Domain.Services.RefreshTokenPart.Interfaces;

namespace API.Application.IdentityCase.Services
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

        public async Task<Result<TokenResult>> RegisterByPhoneAsync(MerchantRegisterByPhoneOptions opt)
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
                    return Result<TokenResult>.Fail(isValid.Code,isValid.Message);
                }

                var dto = new ShopAdminCreateDto(opt.Phone,opt.Password,_clientIpService.GetClientIp());

                var result = await _shopAdminRegisterService.Register(dto);
                if (!result.IsSuccess)
                {
                    return Result<TokenResult>.Fail(ResultCode.InfoExist, result.Message);
                }
                var admin = result.Data;
                var merchantReadDto = new AdminReadDto(admin.Phone, admin.Uuid, admin.Account);

                await _eventBus.PublishAsync(new MerchantRegisterEvent(opt.Phone));
                // 访问令牌
                var adminJwt = _jwtHelper.AdminGenerateToken(opt.Phone, result.Data.Uuid, result.Data.Account);
                // 刷新令牌
                var refreshTokenResult = await _refreshTokenCreateService.AddWeekRefreshTokenAsnyc(result.Data.Uuid);
                if (!refreshTokenResult.IsSuccess)
                {
                    return Result<TokenResult>.Fail(ResultCode.ServerError, "创建刷新令牌失败");
                }

                var tokenDto = new TokenResult(adminJwt, refreshTokenResult.Data, 1200, merchantReadDto);

                return Result<TokenResult>.Success(tokenDto);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<TokenResult>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
