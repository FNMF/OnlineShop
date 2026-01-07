using API.Application.RoleCase.Interfaces;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Interfaces;

namespace API.Application.RoleCase.Services
{
    public class RoleService:IRoleService
    {
        private readonly IAdminRoleRepository _adminRoleRepository;
        private readonly ICurrentService _currentService;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IAdminRoleRepository adminRoleRepository,
            ICurrentService currentService,
            ILogger<RoleService> logger)
        {
            _adminRoleRepository = adminRoleRepository;
            _currentService = currentService;
            _logger = logger;
        }
        // 测试阶段获得shop管理员角色，后续要删除或修改
        public async Task<Result> GetShopAdminRoleTest()
        {
            try
            {
                var uuid = _currentService.RequiredUuid;
                var isExist = await _adminRoleRepository.GetRolesByAdminIdAsync(uuid);
                if (isExist != null && isExist.Count > 0)
                {
                    return Result.Fail(ResultCode.InfoExist, "已存在其余身份");
                }

                var isSuccess = await _adminRoleRepository.MarkAsAdmin(uuid);
                if (isSuccess)
                {
                    return Result.Success("成功获得测试商户身份");
                }
                else
                {
                    return Result.Fail(ResultCode.ServerError, "获得测试商户身份失败");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetShopAdminRoleTest");
                return Result.Fail(ResultCode.ServerError, "获得测试商户身份异常");
            }
        }
    }
}
