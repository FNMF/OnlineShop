using API.Common.Models.Results;

namespace API.Common.Helpers
{
    public class ValidationCodeHelper
    {
        public static Dictionary<string ,(string Code, DateTime Expire)> Codes = new();
        
        
        public static string CreateCode(string Phone)
        {
            var code = "655652";
            Codes[Phone] = (code, DateTime.UtcNow.AddMinutes(2));
            return code;

            /*var code = new Random().Next(100000,999999).ToString();
            Codes[Phone] = (code, DateTime.UtcNow.AddMinutes(2));
            return code;*/
        }

        public static Result ValidateCode(string Phone, string Code)
        {
            if(Phone == null || Code == null)
            {
                return Result.Fail(ResultCode.ValidationError, "输入数据不合法");
            }
            if (!Codes.ContainsKey(Phone))
            {
                return Result.Fail(ResultCode.NotExist, "验证码未发送");
            }
            
            var (code,expire) = Codes[Phone];

            if(expire<DateTime.UtcNow)
            {
                return Result.Fail(ResultCode.InfoExpire, "验证码过期");
            }
            if(code != Code)
            {
                return Result.Fail(ResultCode.ValidationError, "验证码错误");
            }

            Codes.Remove(Phone);

            return Result.Success();
        }
    }
}
