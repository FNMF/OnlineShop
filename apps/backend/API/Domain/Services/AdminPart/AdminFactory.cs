using API.Application.Common.DTOs;
using API.Common.Helpers;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.AdminPart
{
    public class AdminFactory
    {
        public static Result<Admin> CreateNoServiceShop(ShopAdminCreateDto dto)
        {
            var validations = new List<Func<ShopAdminCreateDto, bool>>
            {
                o => !string.IsNullOrEmpty(o.Phone)&&o.Phone.Length ==11,
                o => !string.IsNullOrEmpty(o.Password)&&o.Password.Length >8&&o.Password.Length<30,
            };

            var validationMessages = new List<string>();
            foreach (var validation in validations)
            {
                if (!validation(dto))
                {
                    validationMessages.Add("数据不合法");
                }
            }

            if (validationMessages.Any())
            {
                return Result<Admin>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            };

            var hash = PwdHashHelper.HashandSalt(dto.Password);


            var admin = new Admin
            {
                AdminPhone = dto.Phone,
                AdminPwdhash = hash.Hash,
                AdminSalt = hash.Salt,
                AdminLastlogintime = DateTime.Now,
                AdminLastlocation = dto.IpLocation,
                AdminKey = RandomKeyHelper.GetIpKey(dto.IpLocation),
                AdminUuid = UuidV7Helper.NewUuidV7ToBtyes(),
            };

            return Result<Admin>.Success(admin);
        }
    }
}
