namespace API.Application.Common.DTOs
{
    public class MerchantCreateDto
    {
        public string Name { get; }
        public string Province { get; }
        public string City {  get; }
        public string District {  get; }
        public string Detail { get; }
        public DateTime Businessstart { get; }
        public DateTime Businessend { get; }
        public byte[]? Adminuuid {  get; }
        public MerchantCreateDto(string name, string province, string city, string district, string detail, DateTime businessstart, DateTime businessend, byte[]? adminuuid)
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
