using API.Domain.Entities.Models;

namespace API.Application.Common.DTOs
{
    public class MerchantUpdateDto
    {
        public string Name { get; }
        public string Province { get; }
        public string City { get; }
        public string District { get; }
        public string Detail { get; }
        public DateTime Businessstart { get; }
        public DateTime Businessend { get; }
        public byte[] Adminuuid { get; }
        public byte[] MerchantUuid { get; }
        public MerchantUpdateDto(MerchantUpdateDto dto)
        {
            Name = dto.Name;
            Province = dto.Province;
            City = dto.City;
            District = dto.District;
            Detail = dto.Detail;
            Businessstart = dto.Businessstart;
            Businessend = dto.Businessend;
            Adminuuid = dto.Adminuuid;
            MerchantUuid = dto.MerchantUuid;
        }
    }
}
