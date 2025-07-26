namespace API.Application.Common.DTOs
{
    public class CartUpdateDto
    {
        public byte[] UserUuid { get; }
        public byte[] CartUuid { get; }
        public byte[] ProductUuid { get; }
        public int Quantity { get; }

        public CartUpdateDto(CartUpdateDto dto)
        {
            UserUuid = dto.UserUuid;
            CartUuid = dto.CartUuid;
            ProductUuid = dto.ProductUuid;
            Quantity = dto.Quantity;
        }
    }
}
