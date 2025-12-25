using API.Application.IdentityCase.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.IdentityCase.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/shop_admin")]
    public class ShopAdminProfileController:ControllerBase
    {
        /*
        * TODO,添加商户申请成为内测商户接口
        private readonly IMerchantOperateService _merchantOperateService;
        public ShopAdminProfileController(IMerchantOperateService merchantOperateService)
        {
            _merchantOperateService = merchantOperateService;
        }
        [HttpGet("apply/level/in-test")]
        public async Task<IActionResult> ApplyForLevelInTest()
        {

        }*/
        private readonly IShopAdminProfileService _merchantProfileService;
        public ShopAdminProfileController(IShopAdminProfileService merchantProfileService)
        {
            _merchantProfileService = merchantProfileService;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _merchantProfileService.GetShopAdminProfile();
            if (result.IsSuccess)
            {
                return Ok(result); // 返回登录成功的结果（比如 Token 等）
            }
            else
            {
                return Unauthorized(result); // 登录失败的错误信息
            }
        }
    }
}
