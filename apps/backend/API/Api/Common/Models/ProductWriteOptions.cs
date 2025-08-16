using Microsoft.AspNetCore.Http;

namespace API.Api.Common.Models
{
    public class ProductWriteOptions
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductIngredient { get; set; }
        public string ProductWeight { get; set; }
        public bool ProductIslisted { get; set; }
        public bool ProductIsavailable { get; set; }
        public IFormFile? ProductCoverFile { get; set; }
        public List<ImageSortDto>? ProductImages { get; set; }

        public class ImageSortDto
        {
            public IFormFile ProductImage { get; set; }
            public int SortNumber { get; set; }
        } 
    }
}
