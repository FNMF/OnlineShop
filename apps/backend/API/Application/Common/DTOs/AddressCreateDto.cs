namespace API.Application.Common.DTOs
{
    public class AddressCreateDto
    {
        public string Name { get; }
        public string Phone { get; }
        public string Province { get; }
        public string City { get; }
        public string District { get; }
        public string Detail { get; }
        public byte[] UserUuid { get; }

        public AddressCreateDto(string name, string phone, string province, string city, string district, string detail, byte[] userUuid)
        {
            Name = name;
            Phone = phone;
            Province = province;
            City = city;
            District = district;
            Detail = detail;
            UserUuid = userUuid;
        }

    }
}
