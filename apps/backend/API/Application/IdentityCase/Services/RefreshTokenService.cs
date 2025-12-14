using API.Application.IdentityCase.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Services.AdminPart.Interfaces;
using API.Domain.Services.MerchantPart.Interfaces;
using API.Domain.Services.RefreshTokenPart.Interfaces;

namespace API.Application.IdentityCase.Services
{
    public class RefreshTokenService: IRefreshTokenService
    {
        private readonly IRefreshTokenReadService _refreshTokenReadService;
        private readonly IShopAdminReadService _shopAdminReadService;
        private readonly JwtHelper _jwtHelper;
        private readonly ICurrentService _currentService;
        private readonly ILogger<RefreshTokenService> _logger;
        public RefreshTokenService(IRefreshTokenReadService refreshTokenReadService, IShopAdminReadService shopAdminReadService, JwtHelper jwtHelper, ICurrentService currentService, ILogger<RefreshTokenService> logger)
        {
            _refreshTokenReadService = refreshTokenReadService;
            _shopAdminReadService = shopAdminReadService;
            _jwtHelper = jwtHelper;
            _currentService = currentService;
            _logger = logger;
        }

        public async Task<Result<String>> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                var uuid = _currentService.RequiredUuid;
                var verifyResult = await _refreshTokenReadService.VerifyToken(uuid, refreshToken);
                if (!verifyResult.IsSuccess)
                {
                    return Result<string>.Fail(verifyResult.Code, verifyResult.Message);
                }

                var shopAdminResult = await _shopAdminReadService.GetAdminByUuid(uuid);
                if (!shopAdminResult.IsSuccess)
                {
                    return Result<String>.Fail(shopAdminResult.Code, shopAdminResult.Message);
                }

                var shopAdmin = shopAdminResult.Data;

                var newAccessToken = _jwtHelper.AdminGenerateToken(shopAdmin.Phone, shopAdmin.Uuid, shopAdmin.Account);
                return Result<string>.Success(newAccessToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<string>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
