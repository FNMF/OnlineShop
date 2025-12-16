using API.Common.Helpers;
using API.Domain.Services.External;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.IdentityCase.Controllers
{
    [Route("api/auth/code")]
    [ApiController]
    public class ValidationCodeController:ControllerBase
    {
        private readonly IValidationCodeService _validationCodeService;
        public ValidationCodeController(IValidationCodeService validationCodeService)
        {
            _validationCodeService = validationCodeService;
        }
        [HttpPost("apply")]
        public async Task<IActionResult> CodeApply([FromBody] String phone)
        {
            var isValied = PhoneNumberFactory.ValidationPhoneNumber(phone);
            if (!isValied.IsSuccess)
            {
                return BadRequest(isValied);
            }
            var result = await _validationCodeService.GenerateValidationCodeAsync(phone);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
