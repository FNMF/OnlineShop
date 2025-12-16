using API.Common.Helpers;
using API.Common.Models.Results;

namespace API.Domain.Services.External
{
    public class ValidationCodeServicev: IValidationCodeService
    {
        public async Task<Result> GenerateValidationCodeAsync(string phone)
        {
            var code = ValidationCodeHelper.CreateCode(phone);
            //TODO,添加第三方验证码服务
            return Result.Success(code);
        }
    }
}
