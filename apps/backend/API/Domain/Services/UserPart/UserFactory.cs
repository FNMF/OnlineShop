using API.Application.Common.DTOs;
using API.Common.Helpers;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.UserPart
{
    public class UserFactory
    {
        public static Result<User> Create(UserCreateDto dto)
        {
            var validations = new List<Func<UserCreateDto, bool>>
            {
                o => !string.IsNullOrEmpty(o.Name)&&o.Name.Length<20,
                o => !string.IsNullOrEmpty(o.Phone)&&o.Phone.Length!=11&&!o.Phone.All(char.IsDigit),
                o => !string.IsNullOrEmpty(o.OpenId)&&o.OpenId.Length!=28,
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
                return Result<User>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            };

            var user = new User
            {
                UserName = dto.Name,
                UserOpenid = dto.OpenId,
                UserPhone = dto.Phone,
                UserBp = 0,
                UserCredit = 50,
                UserCreatedat = DateTime.Now,
                UserIsdeleted = false,
                UserUuid = UuidV7Helper.NewUuidV7ToBtyes(),
            };

            return Result<User>.Success(user);
        }

        public static Result<User> Update(UserUpdateDto dto)
        {
            var validations = new List<Func<UserUpdateDto, bool>>
            {
                o => !string.IsNullOrEmpty(o.Name)&&o.Name.Length<20,
                o => !string.IsNullOrEmpty(o.Phone)&&o.Phone.Length!=11&&!o.Phone.All(char.IsDigit),
                o => !string.IsNullOrEmpty(o.OpenId)&&o.OpenId.Length!=28,
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
                return Result<User>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            };

            var user = new User
            {
                UserName = dto.Name,
                UserOpenid = dto.OpenId,
                UserPhone = dto.Phone,
                UserBp =  dto.BpChange,
                UserCredit = dto.CreditChange,
            };

            return Result<User>.Success(user);
        }
    }
}
