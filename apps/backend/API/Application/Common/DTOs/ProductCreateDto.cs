namespace API.Application.Common.DTOs
{
    public class ProductCreateDto
    {
        public Guid Uuid { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Stock { get; }
        public string Description { get; }
        public string Ingredient { get; }
        public string Weight { get; }
        public bool Islisted { get; }
        public Guid MerchantUuid { get; }
        public string CoverUrl {  get; }
        public decimal PackingFee { get; }

        public ProductCreateDto(Guid uuid,string name , decimal price, int stock ,string description , string ingredient, string weight, bool isListed, Guid merchantUuid, string coverUrl, decimal packingFee)
        {
            Uuid = uuid;
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
            Ingredient = ingredient;
            Weight = weight;
            Islisted = isListed;
            MerchantUuid = merchantUuid;
            CoverUrl = coverUrl;
            PackingFee = packingFee;
        }
    }
}
