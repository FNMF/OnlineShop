using API.Application.MerchantCase.Interfaces;
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

        /*[HttpPost("")]
        [Authorize]*/

    }
}
