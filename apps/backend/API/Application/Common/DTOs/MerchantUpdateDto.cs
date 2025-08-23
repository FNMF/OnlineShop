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
        public TimeOnly Businessstart { get; }
        public TimeOnly Businessend { get; }
        public Guid Adminuuid { get; }
        public Guid MerchantUuid { get; }
        public MerchantUpdateDto(string name, string province, string city, string district, string detail, TimeOnly businessstart, TimeOnly businessend, Guid adminuuid, Guid merchantUuid)
        {
            Name = name;
            Province = province;
            City = city;
            District = district;
            Detail = detail;
            Businessstart = businessstart;
            Businessend = businessend;
            Adminuuid = adminuuid;
            MerchantUuid = merchantUuid;
        }
    }
}
