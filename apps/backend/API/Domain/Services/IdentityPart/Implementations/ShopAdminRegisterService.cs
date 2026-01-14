using API.Application.Common.DTOs;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.AdminPart;
using API.Domain.Services.IdentityPart.Interfaces;
using System.Formats.Asn1;

namespace API.Domain.Services.IdentityPart.Implementations
{
    public class ShopAdminRegisterService: IShopAdminRegisterService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly JwtHelper _jwtHelper;
        private readonly ILogger<ShopAdminRegisterService> _logger;

        public ShopAdminRegisterService(IAdminRepository adminRepository, JwtHelper jwtHelper, ILogger<ShopAdminRegisterService> logger)
        {
            _adminRepository = adminRepository;
            _jwtHelper = jwtHelper;
            _logger = logger;
        }

        public async Task<Result<Admin>> Register(ShopAdminCreateDto dto)
        {
            try
            {
                var admin = await _adminRepository.GetAdminByPhoneAsync(dto.Phone);

                if (admin != null) 
                {
                    _logger.LogWarning("已存在该用户");
                    return Result<Admin>.Fail(ResultCode.InfoExist, "手机号已被注册");
                }

                admin = AdminFactory.CreateNoServiceShop(dto).Data;

                await _adminRepository.AddAdminAsync(admin);

                await _adminRepository.SetAsNoServiceAdmin(admin);

                return Result<Admin>.Success(admin); 

            }catch (Exception ex)
            {
                _logger.LogError(ex, "创建管理员时出错");
                return Result<Admin>.Fail(ResultCode.ServerError, ex.Message);
            }
        }

    }
}
