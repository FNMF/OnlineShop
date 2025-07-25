using API.Application.Common.DTOs;
using API.Common.Helpers;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.CartPart
{
    public class CartFactory
    {
        public static Result<Cart> Create(CartCreateDto dto)
        {
            var validations = new List<Func<CartCreateDto, bool>>
            {
                o => o.Quantity>0,
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
                return Result<Cart>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            };

            var cart = new Cart
            {
                CartUseruuid = dto.UserUuid,
                CartProductuuid = dto.ProductUuid,
                CartQuantity = dto.Quantity,
                CartUuid = UuidV7Helper.NewUuidV7ToBtyes(),
                CartTime = DateTime.Now,
            };

            return Result<Cart>.Success(cart);
        }

        public static Result<Cart> Update(CartUpdateDto dto)
        {
            var validations = new List<Func<CartUpdateDto, bool>>
            {
                o => o.Quantity>0,
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
                return Result<Cart>.Fail(ResultCode.ValidationError, string.Join(", ", validationMessages));
            };

            var cart = new Cart
            {
                CartUseruuid = dto.UserUuid,
                CartProductuuid = dto.ProductUuid,
                CartQuantity = dto.Quantity,
                CartUuid = dto.CartUuid,
                CartTime = DateTime.Now,
            };

            return Result<Cart>.Success(cart);
        }
    }
}
