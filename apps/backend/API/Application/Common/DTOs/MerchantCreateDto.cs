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
        public MerchantCreateDto(string name, string province, string city, string district, string detail, TimeOnly businessstart, TimeOnly businessend, byte[] adminuuid)
        {
            Name = name;
            Province = province;
            City = city;
            District = district;
            Detail = detail;
            Businessstart = businessstart;
            Businessend = businessend;
            Adminuuid = adminuuid;
        }
    }
}
