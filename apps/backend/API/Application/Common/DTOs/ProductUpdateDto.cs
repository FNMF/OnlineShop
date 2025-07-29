namespace API.Application.Common.DTOs
{
    public class ProductUpdateDto
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Stock { get; }
        public string Description { get; }
        public string Ingredient { get; }
        public string Weight { get; }
        public bool Islisted { get; }
        public byte[] Merchantuuid { get; }
        public byte[] ProductUuid { get; }

        public ProductUpdateDto(ProductUpdateDto dto)
        {
            Name = dto.Name;
            Price = dto.Price;
            Stock = dto.Stock;
            Description = dto.Description;
            Ingredient = dto.Ingredient;
            Weight = dto.Weight;
            Merchantuuid = dto.Merchantuuid;
            ProductUuid = dto.ProductUuid;
        }
    }
}

