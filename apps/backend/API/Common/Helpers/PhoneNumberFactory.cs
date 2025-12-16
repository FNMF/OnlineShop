using API.Common.Models.Results;

namespace API.Common.Helpers
{
    public class PhoneNumberFactory
    {
        public static Result<String> ValidationPhoneNumber(string phone)
        {
            // 简单的手机号验证逻辑，可以根据需要进行扩展
            if (string.IsNullOrEmpty(phone))
            {
                return Result<String>.Fail(ResultCode.InvalidInput, "手机号为空");
            }
            // 假设手机号格式为11位数字
            if (phone.Length != 11 || !phone.All(char.IsDigit))
            {
                return Result<String>.Fail(ResultCode.InvalidInput, "手机号格式不正确");
            }
            return Result<String>.Success(phone);
        }
    }
}
