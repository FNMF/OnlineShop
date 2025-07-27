namespace API.Application.Common.DTOs
{
    public class CartUpdateDto
    {
        public byte[] UserUuid { get; }
        public byte[] CartUuid { get; }
        public byte[] ProductUuid { get; }
        public int Quantity { get; }

        public CartUpdateDto(byte[] userUuid, byte[] cartUuid , byte[] productUuid, int quantity)
        {
            UserUuid = userUuid;
            CartUuid = cartUuid;
            ProductUuid = productUuid;
            Quantity = quantity;
        }
    }
}
