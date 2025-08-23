namespace API.Api.Common.Models
{
    public class CartWriteOptions
    { 
        public Guid MerchantUuid {  get; set; }
        public List<CartItemWriteOptions> Items { get; set; } = new List<CartItemWriteOptions>();

        public CartWriteOptions(Guid merchantUuid, List<CartItemWriteOptions> items)
        {
            MerchantUuid = merchantUuid;
            Items = items ?? new List<CartItemWriteOptions>();
        }

        public class CartItemWriteOptions
        {
            public Guid ProductUuid { get; set; }
            public int Quantity {  get; set; }

            public CartItemWriteOptions(Guid productUuid, int quantity)
            {
                ProductUuid = productUuid;
                Quantity = quantity;
            }
        }
    }
}
