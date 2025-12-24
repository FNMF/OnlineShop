using API.Application.MerchantCase.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.MerchantCase.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/merchant")]
    public class MerchantOperateController:ControllerBase
    {
        /*
        * TODO,添加商户申请成为内测商户接口
        private readonly IMerchantOperateService _merchantOperateService;
        public MerchantOperateController(IMerchantOperateService merchantOperateService)
        {
            _merchantOperateService = merchantOperateService;
        }
        [HttpGet("apply/level/in-test")]
        public async Task<IActionResult> ApplyForLevelInTest()
        {

        }*/
        private readonly IMerchantProfileService _merchantProfileService;
        public MerchantOperateController(IMerchantProfileService merchantProfileService)
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
