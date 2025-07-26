namespace API.Application.Common.DTOs
{
    public class CartCreateDto
    {
        public byte[] UserUuid { get; }
        public byte[] ProductUuid { get; }
        public int Quantity { get; }

        public CartCreateDto(CartCreateDto dto)
        {
            UserUuid = dto.UserUuid;
            ProductUuid = dto.ProductUuid;
            Quantity = dto.Quantity;
        }
    }
}
