namespace API.Entities.Dto
{
    public class CURAddressDto
    {
        public string Name { get; set; }
        public string Phone {  get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Detail { get; set; }
        public bool IsDefault { get; set; }
    }
}
