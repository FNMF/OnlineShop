namespace API.Api.Common.Models
{
    public class CartWriteOptions
    { 
        public byte[] MerchantUuid {  get; set; }
        public List<CartItemWriteOptions> Items { get; set; } = new List<CartItemWriteOptions>();

        public CartWriteOptions( byte[] merchantUuid, List<CartItemWriteOptions> items)
        {
            MerchantUuid = merchantUuid;
            Items = items ?? new List<CartItemWriteOptions>();
        }

        public class CartItemWriteOptions
        {
            public byte[] ProductUuid { get; set; }
            public int Quantity {  get; set; }

            public CartItemWriteOptions(byte[] productUuid, int quantity)
            {
                ProductUuid = productUuid;
                Quantity = quantity;
            }
        }
    }
}
