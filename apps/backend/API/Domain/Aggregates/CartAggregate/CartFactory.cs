using API.Common.Models.Results;
using API.Domain.Aggregates.CartAggregate;
using API.Domain.Entities.Models;

namespace API.Domain.Aggregates.CartAggregates
{
    public class CartFactory
    {
        public static Result<CartMain> CreateAggregate(CartMain cartMain)
        {
            if (cartMain == null) 
            {
                return Result<CartMain>.Fail(ResultCode.InvalidInput, "输入为空");
            }
            if(cartMain.Items == null || !cartMain.Items.Any())
            {
                return Result<CartMain>.Fail(ResultCode.InvalidInput, "购物车不能为空");
            }

            return Result<CartMain>.Success(cartMain);
        }
        public static Result<Cart> ToEntity(CartMain cartMain)
        {
            var cart = new Cart
            {
                Uuid = cartMain.CartUuid,
                UserUuid = cartMain.UserUuid,
                MerchantUuid = cartMain.MerchantUuid,
                Cartitems = cartMain.Items.Select(item => new Cartitem
                {
                    CartUuid = cartMain.CartUuid,
                    Uuid = item.CartItemUuid,
                    ProductUuid = item.ProductUuid,
                    ProductPrice = item.Price,
                    PackingFee = item.PackingFee,
                    Quantity = item.Quantity,
                    ProductCover = item.CoverUrl,
                    ProductName = item.ProductName,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false,
                }).ToList()
            };
            return Result<Cart>.Success(cart);
        }
        public static Result<CartMain> ToAggregate(Cart cart)
        {
            var items = cart.Cartitems.Select(item => new CartItem
            (
                item.Uuid,
                item.ProductUuid,
                item.ProductPrice,
                item.PackingFee,
                item.Quantity,
                item.ProductCover,
                item.ProductName
            )).ToList();

            var cartMain = new CartMain
            (
                cart.Uuid,
                cart.UserUuid,
                cart.MerchantUuid,
                items
            );

            return Result<CartMain>.Success( cartMain );
        }
    }
}
