using API.Api.MerchantCase.Models;
using API.Application.MerchantCase.Interfaces;
using API.Domain.Enums;
using API.Infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.MerchantCase.Controllers
{
    [Route("api/merchant")]
    public class MerchantController:ControllerBase
    {
        private readonly IMerchantManagementService _merchantManagementService;
        public MerchantController(IMerchantManagementService merchantManagementService)
        {
            _merchantManagementService = merchantManagementService;
        }

        [HttpPost("")]
        [Authorize]
        [AuthorizePermission(Domain.Enums.RoleName.shop_owner,Domain.Enums.Permissions.AddMerchantShop)]
        public async Task<IActionResult> CreateMerchant([FromBody] MerchantCreateOptions opt)
        {
            var result = await _merchantManagementService.CreateMerchantAsync(opt);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPatch("")]
        [Authorize]
        [AuthorizePermission(Domain.Enums.RoleName.shop_owner,Domain.Enums.Permissions.UpdateMerchantShop)]
        public async Task<IActionResult> UpdateMerchant([FromBody] MerchantUpdateOptions opt)
        {
            var result = await _merchantManagementService.UpdateMerchantAsync(opt);
            if (result.IsSuccess) 
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("")]
        [Authorize]
        [AuthorizePermission(Domain.Enums.RoleName.shop_owner, Permissions.GetMerchantShop)]
        public async Task<IActionResult> GetAdminMerchant()
        {
            var result = await _merchantManagementService.GetMerchantDetailAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        // 给用户用的获取商户信息
        [HttpGet("{uuid}")]
        [Authorize]
        public async Task<IActionResult> GetMerchant(Guid uuid)
        {
            var result = await _merchantManagementService.GetMerchantDetailAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
