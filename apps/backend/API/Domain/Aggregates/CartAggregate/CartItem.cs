using API.Common.Helpers;

namespace API.Domain.Aggregates.CartAggregate
{
    public class CartItem
    {
        public byte[] CartItemUuid { get; private set; }
        public byte[] ProductUuid { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public string CoverUrl { get; private set; }
        public string ProductName { get; private set; }

        public CartItem(byte[] productUuid, decimal price, int quantity, string coverUrl, string productName)
        {
            CartItemUuid = UuidV7Helper.NewUuidV7ToBtyes();
            ProductUuid = productUuid;
            Price = price;
            Quantity = quantity;
            CoverUrl = coverUrl;
            ProductName = productName;
        }
        internal CartItem(byte[] cartItemUuid, byte[] productUuid, decimal price, int quantity, string coverUrl, string productName)
        {
            CartItemUuid = cartItemUuid;
            ProductUuid = productUuid;
            Price = price;
            Quantity = quantity;
            CoverUrl = coverUrl;
            ProductName = productName;
        }

        internal void UpdateQuantity(int amount)
        {
            if ((Quantity + amount) <= 0) throw new ArgumentException("商品数量要大于0", nameof(amount));
            Quantity += amount;
        }

        internal void UpdatePrice(decimal price) 
        {
            Price = price;
        }

    }
}
