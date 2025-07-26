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

        public AddressUpdateDto(AddressUpdateDto dto)
        {
            Name = dto.Name;
            Phone = dto.Phone;
            Province = dto.Province;
            City = dto.City;
            District = dto.District;
            Detail = dto.Detail;
            UserUuid = dto.UserUuid;
            AddressUuid = dto.AddressUuid;
        }
    }
}
