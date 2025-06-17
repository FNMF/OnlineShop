namespace API.Application.DTOs
{
    public class CUMerchantDto
    {
        public string Name { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Detail { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid AdminUuid { get; set; }


    }
}
