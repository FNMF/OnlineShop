namespace API.Infrastructure.Region
{
    public class DistrictDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CityId { get; set; }
    }
}
