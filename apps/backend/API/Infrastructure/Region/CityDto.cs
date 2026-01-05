namespace API.Infrastructure.Region
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ProvinceId { get; set; }
    }
}
