namespace API.Api.Models
{
    public class AddressCreateOptions
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Detail { get; set; }
        public bool IsDefault { get; set; }
        public byte[] UuidBytes { get; set; }
    }
}

