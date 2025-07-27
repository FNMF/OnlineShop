namespace API.Application.Common.DTOs
{
    public class AddressUpdateDto
    {
        public string Name { get; }
        public string Phone { get; }
        public string Province { get; }
        public string City { get; }
        public string District { get; } 
        public string Detail { get; }
        public byte[] UserUuid { get; }
        public byte[] AddressUuid { get; }

        public AddressUpdateDto(string name, string phone, string province, string city, string district, string detail, byte[] userUuid, byte[] addressUuid)
        {
            Name = name;
            Phone = phone;
            Province = province;
            City = city;
            District = district;
            Detail = detail;
            UserUuid = userUuid;
            AddressUuid = addressUuid;
        }
    }
}
