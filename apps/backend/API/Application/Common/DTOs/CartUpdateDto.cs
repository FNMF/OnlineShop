namespace API.Application.Common.DTOs
{
    public class CartUpdateDto
    {
        public byte[] UserUuid { get; }
        public byte[] CartUuid { get; }
        public byte[] ProductUuid { get; }
        public int Quantity { get; }

        public CartUpdateDto(byte[] userUuid,  byte[] productUuid, int quantity, byte[] cartUuid)
        {
            UserUuid = userUuid;
            ProductUuid = productUuid;
            Quantity = quantity;
            CartUuid = cartUuid;
        }
    }
}
