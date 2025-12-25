using API.Application.Common.DTOs;
using API.Application.IdentityCase.DTOs;
using API.Application.IdentityCase.Interfaces;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Services.AdminPart.Interfaces;
using Microsoft.AspNetCore.Identity.Data;

namespace API.Application.IdentityCase.Services
{
    public class ShopAdminProfileService:IShopAdminProfileService
    {
        private readonly IShopAdminReadService _shopAdminReadService;
        private readonly ICurrentService _currentService;
        private readonly JwtHelper _jwtHelper;
        private readonly ILogger<ShopAdminProfileService> _logger;
        public ShopAdminProfileService(IShopAdminReadService shopAdminReadService, ICurrentService currentService, JwtHelper jwtHelper, ILogger<ShopAdminProfileService> logger)
        {
            _shopAdminReadService = shopAdminReadService;
            _currentService = currentService;
            _jwtHelper = jwtHelper;
            _logger = logger;
        }
        public async Task<Result<AuthResult>> GetShopAdminProfile()
        {
            try
            {
                var uuid = _currentService.RequiredUuid;
                var adminResult = await _shopAdminReadService.GetAdminByUuid(uuid);
                if (!adminResult.IsSuccess)
                {
                    return Result<AuthResult>.Fail(adminResult.Code, adminResult.Message);
                }

                var shopAdmin = adminResult.Data;
                var shopAdminReadDto = new AdminReadDto(shopAdmin.Phone, uuid, shopAdmin.Account);

                var adminJwt = _jwtHelper.AdminGenerateToken(shopAdmin.Phone, uuid, shopAdmin.Account);
                var tokenDto = new LoginTokenResult(adminJwt, null, 1200, shopAdminReadDto);

                var authDto = new AuthResult(false, tokenDto, null);

                return Result<AuthResult>.Success(authDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取商户管理员资料时发生异常");
                return Result<AuthResult>.Fail(ResultCode.ServerError, "获取商户管理员资料时发生异常");
            }
        }
    }
}
