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
                CartUuid = cartMain.CartUuid,
                CartUseruuid = cartMain.UserUuid,
                CartMerchantuuid = cartMain.MerchantUuid,
                Cartitems = cartMain.Items.Select(item => new Cartitem
                {
                    CartitemCartuuid = cartMain.CartUuid,
                    CartitemUuid = item.CartItemUuid,
                    CartitemProductuuid = item.ProductUuid,
                    CartitemProductprice = item.Price,
                    CartitemQuantity = item.Quantity,
                    CartitemProductcover = item.CoverUrl,
                    CartitemProductname = item.ProductName,
                    CartitemCreatedat = DateTime.Now,
                    CartitemIsdeleted = false,
                }).ToList()
            };
            return Result<Cart>.Success(cart);
        }
        public static Result<CartMain> ToAggregate(Cart cart)
        {
            var items = cart.Cartitems.Select(item => new CartItem
            (
                item.CartitemUuid,
                item.CartitemProductuuid,
                item.CartitemProductprice,
                item.CartitemQuantity,
                item.CartitemProductcover,
                item.CartitemProductname
            )).ToList();

            var cartMain = new CartMain
            (
                cart.CartUuid,
                cart.CartUseruuid,
                cart.CartMerchantuuid,
                items
            );

            return Result<CartMain>.Success( cartMain );
        }
    }
}
