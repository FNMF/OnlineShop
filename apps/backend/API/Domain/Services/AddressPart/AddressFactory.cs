using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Helpers;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.AddressPart
{
    public class AddressFactory
    {
        public static Result<Address> Create(AddressCreateDto dto)
        {
            var validations = new List<Func<AddressCreateDto, bool>>
            {
                o => !string.IsNullOrEmpty(o.Name)&&o.Name.Length<20,
                o => ! string.IsNullOrEmpty(o.Phone) && o.Phone.Length != 11&&!o.Phone.All(char.IsDigit),
                o => !string.IsNullOrEmpty(o.Province)&&o.Province.Length<20,
                o => !string.IsNullOrEmpty(o.City)&&o.City.Length<20,
                o => !string.IsNullOrEmpty(o.District)&&o.District.Length<20,
                o => !string.IsNullOrEmpty(o.Detail)&&o.Detail.Length<100,
            };

            var validationMessages = new List<string>();
            foreach (var validation in validations)
            {
                if(!validation(dto)) 
                {
                    validationMessages.Add("数据不合法");
                }
            }

            if (validationMessages.Any())
            {
                return Result<Address>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            };

            var address = new Address
            {
                AddressName = dto.Name,
                AddressPhone = dto.Phone,
                AddressProvince = dto.Province,
                AddressCity = dto.City,
                AddressDistrict = dto.District,
                AddressDetail = dto.Detail,
                AddressUseruuid = dto.UserUuid,
                AddressUuid = UuidV7Helper.NewUuidV7(),
                AddressTime = DateTime.Now,
                AddressIsdeleted = false,
            };

            return Result<Address>.Success(address);
        }

        public static Result<Address> Update(AddressUpdateDto dto)
        {
            var validations = new List<Func<AddressUpdateDto, bool>>
            {
                o => !string.IsNullOrEmpty(o.Name)&&o.Name.Length<20,
                o => ! string.IsNullOrEmpty(o.Phone) && o.Phone.Length != 11&&!o.Phone.All(char.IsDigit),
                o => !string.IsNullOrEmpty(o.Province)&&o.Province.Length<20,
                o => !string.IsNullOrEmpty(o.City)&&o.City.Length<20,
                o => !string.IsNullOrEmpty(o.District)&&o.District.Length<20,
                o => !string.IsNullOrEmpty(o.Detail)&&o.Detail.Length<100,
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
                return Result<Address>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            };

            var address = new Address
            {
                AddressName = dto.Name,
                AddressPhone = dto.Phone,
                AddressProvince = dto.Province,
                AddressCity = dto.City,
                AddressDistrict = dto.District,
                AddressDetail = dto.Detail,
                AddressUseruuid = dto.UserUuid,
                AddressUuid = dto.AddressUuid,
                AddressTime = DateTime.Now,
                AddressIsdeleted = false,
            };

            return Result<Address>.Success(address);
        }
    }
}
