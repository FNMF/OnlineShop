﻿using API.Application.Common.DTOs;
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
                Name = dto.Name,
                Province = dto.Province,
                City = dto.City,
                District = dto.District,
                Detail = AESHelper.Encrypt(dto.Detail),
                BusinessStart = dto.Businessstart,
                BusinessEnd = dto.Businessend,
                AdminUuid = dto.Adminuuid,
                Uuid = UuidV7Helper.NewUuidV7(),
                IsClosed = false,
                IsDeleted = false,
                IsAudited = false,
                
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
                Name = dto.Name,
                Province = dto.Province,
                City = dto.City,
                District = dto.District,
                Detail = AESHelper.Encrypt(dto.Detail),
                BusinessStart = dto.Businessstart,
                BusinessEnd = dto.Businessend,
                AdminUuid = dto.Adminuuid,
                Uuid = dto.MerchantUuid,
                IsClosed = false,
                IsDeleted = false,
                IsAudited = false,
            };

            return Result<Merchant>.Success(merchant);
        }
    }
}
