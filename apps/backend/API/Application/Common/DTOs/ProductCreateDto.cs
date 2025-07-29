namespace API.Application.Common.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Stock { get; }
        public string Description { get; }
        public string Ingredient { get; }
        public string Weight { get; }
        public bool Islisted { get; }
        public byte[] Merchantuuid { get; }

        public ProductCreateDto(ProductCreateDto dto)
        {
            Name = dto.Name;
            Price = dto.Price;
            Stock = dto.Stock;
            Description = dto.Description;
            Ingredient = dto.Ingredient;
            Weight = dto.Weight;
            Islisted = dto.Islisted;
            Merchantuuid = dto.Merchantuuid;
        }
    }
}
