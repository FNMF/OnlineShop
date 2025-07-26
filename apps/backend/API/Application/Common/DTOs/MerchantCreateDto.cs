namespace API.Application.Common.DTOs
{
    public class MerchantCreateDto
    {
        public string Name { get; }
        public string Province { get; }
        public string City {  get; }
        public string District {  get; }
        public string Detail { get; }
        public TimeOnly Businessstart { get; }
        public TimeOnly Businessend { get; }
        public byte[] Adminuuid {  get; }
        public MerchantCreateDto(MerchantCreateDto dto)
        {
            Name = dto.Name;
            Province = dto.Province;
            City = dto.City;
            District = dto.District;
            Detail = dto.Detail;
            Businessstart = dto.Businessstart;
            Businessend = dto.Businessend;
            Adminuuid = dto.Adminuuid;
        }
    }
}
