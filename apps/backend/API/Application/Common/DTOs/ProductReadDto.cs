namespace API.Application.Common.DTOs
{
    public class ProductReadDto     // 这是一个简化的产品读取数据传输对象，用于列表展示
    {
        public Guid ProductUuid { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Stock { get; }
        public string Weight { get; }
        public bool Islisted { get; }
        public bool IsAvailable { get; }
        public string? CoverUrl { get; }
        public decimal PackingFee {  get; }

        public ProductReadDto(Guid productUuid, string name, decimal price, int stock, string weight, bool isListed, bool isAvailable, string coverUrl, decimal packingFee)
        {
            ProductUuid = productUuid;
            Name = name;
            Price = price;
            Stock = stock;
            Weight = weight;
            Islisted = isListed;
            IsAvailable = isAvailable;
            CoverUrl = coverUrl;
            PackingFee = packingFee;
        }

    }
}
