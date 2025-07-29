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
        public byte[] MerchantUuid { get; }
        public string CoverUrl { get; }

        public ProductCreateDto(string name, decimal price, int stock, string description, string ingredient, string weight, bool isListed, byte[] merchantUuid, string coverUrl)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
            Ingredient = ingredient;
            Weight = weight;
            Islisted = isListed;
            MerchantUuid = merchantUuid;
            CoverUrl = coverUrl;
        }
    }
}
