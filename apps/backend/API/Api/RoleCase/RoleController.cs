using API.Application.RoleCase.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.RoleCase
{
    [Route("api/role")]
    public class RoleController:ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleService.GetShopAdminRoleTest();
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
    }
}
