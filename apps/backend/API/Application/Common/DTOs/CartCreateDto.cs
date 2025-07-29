namespace API.Application.Common.DTOs
{
    public class CartCreateDto
    {
        public byte[] UserUuid { get; }
        public byte[] ProductUuid { get; }
        public int Quantity { get; }

        public CartCreateDto(byte[] userUuid, byte[] productUuid, int quantity)
        {
            UserUuid = userUuid;
            ProductUuid = productUuid;
            Quantity = quantity;
        }
    }
}
