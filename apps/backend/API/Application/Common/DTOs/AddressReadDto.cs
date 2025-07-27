namespace API.Application.Common.DTOs
{
    public class AddressReadDto
    {
        public byte[] AddressUuid { get; }
        public string Name { get; }
        public string Phone {  get; }
        public string Province { get; }
        public string City { get; }
        public string District { get; }
        public string Detail {  get; }
        public AddressReadDto(byte[] addressUuid,string name, string phone, string province, string city, string district,string detail)
        {
            AddressUuid = addressUuid;
            Name = name;
            Phone = phone;
            Province = province;
            City = city;
            District = district;
            Detail = detail;
        }
    }
}
