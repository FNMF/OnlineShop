using API.Application.RoleCase.Interfaces;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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
        public async Task<Result> ApplyShopAdminRoleTest()
        {
            try
            {
                var uuid = _currentService.RequiredUuid;
                var rolesResult =await GetRoles();
                var roles = rolesResult.Data;
                if (roles.Contains("shop_owner"))
                {
                    return Result.Fail(ResultCode.InfoExist, "已拥有测试商户身份");
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
                _logger.LogError(ex, "获得测试商户时发生错误");
                return Result.Fail(ResultCode.ServerError, "获得测试商户身份异常");
            }
        }
        public async Task<Result<List<string>>> GetRoles()
        {
            var uuid = _currentService.RequiredUuid;
            var rolesResult = await _adminRoleRepository.GetRolesByAdminIdAsync(uuid)
                          ?? new List<Role>();
            var roles = rolesResult.Select(r => r.Name).ToList();
            return Result<List<string>>.Success(roles);
        }
    }
}
