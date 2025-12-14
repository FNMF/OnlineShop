using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Interfaces;
using API.Domain.Services.AdminPart.Interfaces;

namespace API.Domain.Services.AdminPart.Implementations
{
    public class ShopAdminReadService : IShopAdminReadService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ILogger<ShopAdminReadService> _logger;

        public ShopAdminReadService(IAdminRepository adminRepository, ILogger<ShopAdminReadService> logger)
        {
            _adminRepository = adminRepository;
            _logger = logger;
        }

        public async Task<Result<AdminReadDto>> GetAdminByUuid(Guid uuid)
        {
            try
            {
                var admin = await _adminRepository.GetAdminByUuidAsync(uuid);
                if (admin == null)
                {
                    _logger.LogWarning("管理员不存在");
                    return Result<AdminReadDto>.Fail(ResultCode.NotExist, "管理员不存在");
                }
                var dto = new AdminReadDto(
                    admin.Phone,
                    admin.Uuid,
                    admin.Account);
                
                return Result<AdminReadDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取管理员信息时出错");
                return Result<AdminReadDto>.Fail(ResultCode.ServerError, ex.Message);
            }
        }
    }
}
