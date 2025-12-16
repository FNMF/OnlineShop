using API.Api.IdentityCase.Models;
using API.Application.Common.DTOs;
using API.Application.Common.EventBus;
using API.Application.IdentityCase.DTOs;
using API.Application.IdentityCase.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Events.MerchantCase;
using API.Domain.Events.PlatformCase;
using API.Domain.Services.AdminPart.Interfaces;
using API.Domain.Services.IdentityPart.Interfaces;
using API.Domain.Services.RefreshTokenPart.Implementations;
using API.Domain.Services.RefreshTokenPart.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Application.IdentityCase.Services
{

    public class MerchantLoginService : IMerchantLoginService
    {
        private readonly IAdminPasswordVerifyService _adminPasswordVerifyService;
        private readonly IRefreshTokenReadService _refreshTokenReadService;
        private readonly IShopAdminReadService _shopAdminReadService;
        private readonly IRefreshTokenCreateService _refreshTokenCreateService;
        private readonly ICurrentService _currentService;
        private readonly JwtHelper _jwtHelper;
        private readonly EventBus _eventBus;
        private readonly ILogger<MerchantLoginService> _logger;

        public MerchantLoginService(IAdminPasswordVerifyService adminPasswordVerifyService, IRefreshTokenReadService refreshTokenReadService, IShopAdminReadService shopAdminReadService, IRefreshTokenCreateService refreshTokenCreateService, ICurrentService currentService, JwtHelper jwtHelper, EventBus eventBus, ILogger<MerchantLoginService> logger)
        {
            _adminPasswordVerifyService = adminPasswordVerifyService;
            _refreshTokenReadService = refreshTokenReadService;
            _shopAdminReadService = shopAdminReadService;
            _refreshTokenCreateService = refreshTokenCreateService;
            _currentService = currentService;
            _jwtHelper = jwtHelper;
            _eventBus = eventBus;
            _logger = logger;
        }
        public async Task<Result<TokenResult>> LoginByTokenAsync(string? accessToken, string refreshToken)
        {
            try
            {
                if (string.IsNullOrEmpty(accessToken)||string.IsNullOrEmpty(refreshToken))
                {
                    return Result<TokenResult>.Fail(ResultCode.InvalidInput, "无效的输入");
                }

                var uuid = _currentService.RequiredUuid;

                var verifyResult = await _refreshTokenReadService.VerifyToken(uuid, refreshToken);
                if (!verifyResult.IsSuccess)
                {
                    return Result<TokenResult>.Fail(verifyResult.Code, verifyResult.Message);
                }

                var result = await _shopAdminReadService.GetAdminByUuid(uuid);
                var admin = result.Data;
                var merchantReadDto = new AdminReadDto(admin.Phone, admin.Uuid, admin.Account);

                var adminJwt = _jwtHelper.AdminGenerateToken(admin.Phone, admin.Uuid, admin.Account);

                var tokenDto = new TokenResult(
                    adminJwt,
                    refreshToken,
                    1200,
                    merchantReadDto
                );
                await _eventBus.PublishAsync(new MerchantLoginEvent(uuid));

                return Result<TokenResult>.Success(tokenDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<TokenResult>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
        public async Task<Result<TokenResult>> LoginByAccountAsync(MerchantLoginByAccountOptions opt)
        {
            try
            {
                // 1. 调用 Domain 层的服务来验证账号和密码
                var isValid = await _adminPasswordVerifyService.VerifyShopPasswordAsync(opt.Account, opt.Password);

                if (!isValid.IsSuccess)
                {
                    // 2. 密码验证失败，返回错误信息
                    return Result<TokenResult>.Fail(ResultCode.LoginVerifyError, "用户名或密码错误");
                }

                // 3. 登录成功，生成一些登录后的业务操作，比如生成 Token 或者事件处理
                // 例如，你可以通过 EventPublisher 触发一些事件

                var uuidString = _jwtHelper.AnalysisToken(isValid.Message, ClaimTypes.NameIdentifier).Message;
                var uuid = Guid.Parse(uuidString);

                var result = await _shopAdminReadService.GetAdminByUuid(uuid);
                var admin = result.Data;
                var merchantReadDto = new AdminReadDto(admin.Phone, admin.Uuid, admin.Account);

                var adminJwt = _jwtHelper.AdminGenerateToken(admin.Phone, admin.Uuid, admin.Account);

                var refreshTokenResult = await _refreshTokenCreateService.AddWeekRefreshTokenAsnyc(result.Data.Uuid);
                if (!refreshTokenResult.IsSuccess)
                {
                    return Result<TokenResult>.Fail(ResultCode.ServerError, "创建刷新令牌失败");
                }

                var tokenDto = new TokenResult(
                adminJwt,
                    refreshTokenResult.Data,
                    1200,
                    merchantReadDto
                );

                await _eventBus.PublishAsync(new MerchantLoginEvent(uuid));

                return Result<TokenResult>.Success(tokenDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<TokenResult>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
        public async Task<Result<TokenResult>> LoginByPhoneAsync(MerchantLoginByValidationCodeOptions opt)
        {
            try
            {
                // 1. 调用 ValidationCodeHelper 服务来验证手机号和验证码
                var isValid = ValidationCodeHelper.ValidateCode(opt.Phone,opt.ValidationCode);
                if (!isValid.IsSuccess)
                {
                    // 2. 验证失败，返回错误信息
                    return Result<TokenResult>.Fail(ResultCode.LoginVerifyError, "手机号或验证码错误");
                }
                // 3. 登录成功，生成一些登录后的业务操作，比如生成 Token 或者事件处理
                var uuidString = _jwtHelper.AnalysisToken(isValid.Message, ClaimTypes.NameIdentifier).Message;
                var uuid = Guid.Parse(uuidString);

                var result = await _shopAdminReadService.GetAdminByUuid(uuid);
                var admin = result.Data;
                var merchantReadDto = new AdminReadDto(admin.Phone, admin.Uuid, admin.Account);

                var adminJwt = _jwtHelper.AdminGenerateToken(admin.Phone, admin.Uuid, admin.Account);

                var refreshTokenResult = await _refreshTokenCreateService.AddWeekRefreshTokenAsnyc(result.Data.Uuid);
                if (!refreshTokenResult.IsSuccess)
                {
                    return Result<TokenResult>.Fail(ResultCode.ServerError, "创建刷新令牌失败");
                }

                var tokenDto = new TokenResult(
                    adminJwt,
                    refreshTokenResult.Data,
                    1200,
                    merchantReadDto
                );

                await _eventBus.PublishAsync(new MerchantLoginEvent(uuid));
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
