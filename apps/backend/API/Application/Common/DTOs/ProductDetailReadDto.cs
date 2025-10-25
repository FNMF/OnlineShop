namespace API.Application.Common.DTOs
{
    public class ProductDetailReadDto
    {
        public Guid ProductUuid { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Stock { get; }
        public string? Description { get; }
        public string? Ingredient { get; }
        public string Weight { get; }
        public bool Islisted { get; }
        public bool IsAvailable { get; }
        public string? CoverUrl { get; }
        public decimal PackingFee {  get; }
        public List<string> Images { get; }

        public ProductDetailReadDto(Guid productUuid, string name, decimal price, int stock, string description, string ingredient, string weight, bool isListed, bool isAvailable, string coverUrl,decimal packingFee, List<string> images)
        {
            ProductUuid = productUuid;
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
            Ingredient = ingredient;
            Weight = weight;
            Islisted = isListed;
            IsAvailable = isAvailable;
            CoverUrl = coverUrl;
            PackingFee = packingFee;
            Images = images;
        }
    }
}
