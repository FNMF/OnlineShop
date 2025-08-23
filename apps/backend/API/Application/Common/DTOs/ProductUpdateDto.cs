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
        public Guid MerchantUuid { get; }
        public Guid ProductUuid { get; }
        public string CoverUrl {  get; }

        public ProductUpdateDto(string name, decimal price, int stock, string description, string ingredient, string weight, bool isListed, Guid merchantUuid,Guid productUuid, string coverUrl)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
            Ingredient = ingredient;
            Weight = weight;
            Islisted = isListed;
            MerchantUuid = merchantUuid;
            ProductUuid = productUuid;
            CoverUrl = coverUrl;
        }
    }
}