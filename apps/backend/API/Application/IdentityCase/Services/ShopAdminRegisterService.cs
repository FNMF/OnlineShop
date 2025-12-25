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
using API.Domain.Services.RefreshTokenPart.Interfaces;

namespace API.Application.IdentityCase.Services
{
    public class ShopAdminRegisterService : Interfaces.IShopAdminRegisterService
    {
        private readonly EventBus _eventBus;
        private readonly IClientIpService _clientIpService;
        private readonly ICurrentService _currentService;
        private readonly Domain.Services.IdentityPart.Interfaces.IShopAdminRegisterService _shopAdminRegisterService;
        private readonly IRefreshTokenCreateService _refreshTokenCreateService;
        private readonly JwtHelper _jwtHelper;
        private readonly ILogger<ShopAdminRegisterService> _logger;

        public ShopAdminRegisterService(EventBus eventBus, IClientIpService clientIpService, ICurrentService currentService, Domain.Services.IdentityPart.Interfaces.IShopAdminRegisterService shopAdminRegisterService, IRefreshTokenCreateService refreshTokenCreateService, JwtHelper jwtHelper, ILogger<ShopAdminRegisterService> logger)
        {
            _eventBus = eventBus;
            _clientIpService = clientIpService;
            _currentService = currentService;
            _logger = logger;
            _refreshTokenCreateService = refreshTokenCreateService;
            _jwtHelper = jwtHelper;
            _shopAdminRegisterService = shopAdminRegisterService;
        }

        public async Task<Result<AuthResult>> RegisterByPhoneAsync(ShopAdminRegisterByPhoneOptions opt)
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
                    return Result<AuthResult>.Fail(isValid.Code, isValid.Message);
                }

                var dto = new ShopAdminCreateDto(opt.Phone, opt.Password, _clientIpService.GetClientIp());

                var result = await _shopAdminRegisterService.Register(dto);
                if (!result.IsSuccess)
                {
                    return Result<AuthResult>.Fail(ResultCode.InfoExist, result.Message);
                }
                var admin = result.Data;
                var merchantReadDto = new AdminReadDto(admin.Phone, admin.Uuid, admin.Account);

                await _eventBus.PublishAsync(new ShopAdminRegisterEvent(opt.Phone));
                // 访问令牌
                var adminJwt = _jwtHelper.AdminGenerateToken(opt.Phone, result.Data.Uuid, result.Data.Account);
                // 刷新令牌
                var refreshTokenResult = await _refreshTokenCreateService.AddWeekRefreshTokenAsnyc(result.Data.Uuid);
                if (!refreshTokenResult.IsSuccess)
                {
                    return Result<AuthResult>.Fail(ResultCode.ServerError, "创建刷新令牌失败");
                }

                var tokenDto = new LoginTokenResult(adminJwt, refreshTokenResult.Data, 1200, merchantReadDto);
                // 这里的 true 表示这是一个新注册的用户，但是已经完成注册流程，所以算作登录，返回LoginTokenResult
                var authDto = new AuthResult(true, tokenDto, null);
                return Result<AuthResult>.Success(authDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<AuthResult>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
        public async Task<Result<AuthResult>> RegisterByTempAsync(ShopAdminRegisterByTempOptions opt)
        {
            try
            {
                var phone = _currentService.CurrentPhone;
                if (string.IsNullOrEmpty(phone))
                {
                    return Result<AuthResult>.Fail(ResultCode.InvalidInput, "无效的手机号");
                }
                var dto = new ShopAdminCreateDto(phone, opt.Password, _clientIpService.GetClientIp());
                var result = await _shopAdminRegisterService.Register(dto);
                if (!result.IsSuccess)
                {
                    return Result<AuthResult>.Fail(ResultCode.InfoExist, result.Message);
                }
                var admin = result.Data;
                var merchantReadDto = new AdminReadDto(admin.Phone, admin.Uuid, admin.Account);
                await _eventBus.PublishAsync(new ShopAdminRegisterEvent(phone));
                // 访问令牌
                var adminJwt = _jwtHelper.AdminGenerateToken(phone, result.Data.Uuid, result.Data.Account);
                // 刷新令牌
                var refreshTokenResult = await _refreshTokenCreateService.AddWeekRefreshTokenAsnyc(result.Data.Uuid);
                if (!refreshTokenResult.IsSuccess)
                {
                    return Result<AuthResult>.Fail(ResultCode.ServerError, "创建刷新令牌失败");
                }
                var tokenDto = new LoginTokenResult(adminJwt, refreshTokenResult.Data, 1200, merchantReadDto);
                var authDto = new AuthResult(true, tokenDto, null);
                return Result<AuthResult>.Success(authDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<AuthResult>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
