using API.Application.Common.DTOs;
using API.Common.Helpers;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.RegularExpressions;

namespace API.Domain.Services.ProductPart
{
    public class ProductFactory
    {
        public static Result<Product> Create(ProductCreateDto dto)
        {
            var validations = new List<Func<ProductCreateDto, bool>>
            {
                o => !string.IsNullOrEmpty(o.Name)&&o.Name.Length<20,
                o => o.Price>0&&Regex.IsMatch(o.Price.ToString(), @"^(?:\d{1,6}|\d{1,6}\.\d{1,2})$"),
                o => o.Stock>=0,
                o => !string.IsNullOrEmpty(o.Description)&&o.Description.Length<200,
                o => !string.IsNullOrEmpty(o.Ingredient)&&o.Ingredient.Length<100,
                o => !string.IsNullOrEmpty(o.Weight)&&o.Weight.Length<100,
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
                return Result<Product>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            };

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                Description = dto.Description,
                Ingredient = dto.Ingredient,
                Weight = dto.Weight,
                IsListed = dto.Islisted,
                MerchantUuid = dto.MerchantUuid,
                IsAvailable=false,
                CoverUrl= dto.CoverUrl,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                IsAudited = false,
                PackingFee = dto.PackingFee
            };
            return Result<Product>.Success(product);
        }
        public static Result<Product> Update(ProductUpdateDto dto)
        {
            var validations = new List<Func<ProductUpdateDto, bool>>
            {
                o => !string.IsNullOrEmpty(o.Name)&&o.Name.Length<20,
                o => o.Price>0&&Regex.IsMatch(o.Price.ToString(), @"^(?:\d{1,6}|\d{1,6}\.\d{1,2})$"),
                o => o.Stock>=0,
                o => !string.IsNullOrEmpty(o.Description)&&o.Description.Length<200,
                o => !string.IsNullOrEmpty(o.Ingredient)&&o.Ingredient.Length<100,
                o => !string.IsNullOrEmpty(o.Weight)&&o.Weight.Length<100,
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
                return Result<Product>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            }
            ;

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                Description = dto.Description,
                Ingredient = dto.Ingredient,
                Weight = dto.Weight,
                IsListed = dto.Islisted,
                MerchantUuid = dto.MerchantUuid,
                Uuid = dto.ProductUuid,
                IsAvailable = false,
                CoverUrl=dto.CoverUrl,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                IsAudited = false,
                PackingFee = dto.PackingFee
            };

            return Result<Product>.Success(product);
        }
    }
}
