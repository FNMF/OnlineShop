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
        public AddressReadDto(AddressReadDto dto) 
        {
            AddressUuid = dto.AddressUuid;
            Name = dto.Name;
            Phone = dto.Phone;
            Province = dto.Province;
            City = dto.City;
            District = dto.District;
            Detail = dto.Detail;
        }
    }
}
