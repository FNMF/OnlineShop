using API.Application.Common.DTOs;
using API.Common.Helpers;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.MerchantPart
{
    public class MerchantFactory
    {
        public static Result<Merchant> Create(MerchantCreateDto dto)
        {
            var validations = new List<Func<MerchantCreateDto, bool>>
            {
                o => !string.IsNullOrEmpty(o.Name)&&o.Name.Length<20,
                o => !string.IsNullOrEmpty(o.Province)&&o.Province.Length<20,
                o => !string.IsNullOrEmpty(o.City)&&o.City.Length<20,
                o => !string.IsNullOrEmpty(o.District)&&o.District.Length<20,
                o => !string.IsNullOrEmpty(o.Detail)&&o.Detail.Length<200,
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
                return Result<Merchant>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            };

            var merchant = new Merchant
            {
                MerchantName = dto.Name,
                MerchantProvince = dto.Province,
                MerchantCity = dto.City,
                MerchantDistrict = dto.District,
                MerchantDetail = dto.Detail,
                MerchantBusinessstart = dto.Businessstart,
                MerchantBusinessend = dto.Businessend,
                MerchantAdminuuid = dto.Adminuuid,
                MerchantUuid = UuidV7Helper.NewUuidV7ToBtyes(),
                MerchantIsclosed = false,
                MerchantIsdeleted = false,
                MerchantIsaudited = false,
                
            };
            return Result<Merchant>.Success(merchant);
        }
        public static Result<Merchant> Update(MerchantUpdateDto dto)
        {
            var validations = new List<Func<MerchantUpdateDto, bool>>
            {
                o => !string.IsNullOrEmpty(o.Name)&&o.Name.Length<20,
                o => !string.IsNullOrEmpty(o.Province)&&o.Province.Length<20,
                o => !string.IsNullOrEmpty(o.City)&&o.City.Length<20,
                o => !string.IsNullOrEmpty(o.District)&&o.District.Length<20,
                o => !string.IsNullOrEmpty(o.Detail)&&o.Detail.Length<200,
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
                return Result<Merchant>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            }
            ;

            var merchant = new Merchant
            {
                MerchantName = dto.Name,
                MerchantProvince = dto.Province,
                MerchantCity = dto.City,
                MerchantDistrict = dto.District,
                MerchantDetail = dto.Detail,
                MerchantBusinessstart = dto.Businessstart,
                MerchantBusinessend = dto.Businessend,
                MerchantAdminuuid = dto.Adminuuid,
                MerchantUuid = dto.MerchantUuid,
                MerchantIsclosed = false,
                MerchantIsdeleted = false,
                MerchantIsaudited = false,
            };

            return Result<Merchant>.Success(merchant);
        }
    }
}
