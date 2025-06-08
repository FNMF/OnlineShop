using Microsoft.AspNetCore.Http;

namespace API.Entities.Dto
{
    public class CUProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Stock { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public string Ingredient { get; set; }
        public bool IsListed { get; set; }
        public string Weight { get; set; }
        public string CoverURL {  get; set; } 
        public List<ImageDto> Images { get; set; }
        public List<int> ImageIds { get; set; }
    }
}
