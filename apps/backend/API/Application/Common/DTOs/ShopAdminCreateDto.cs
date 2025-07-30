namespace API.Application.Common.DTOs
{
    public class ShopAdminCreateDto
    {
        public string Phone { get; }
        public string Password { get; }
        public string IpLocation { get; }
        public ShopAdminCreateDto(string phone, string password, string ipLocation)
        {
            Phone = phone;
            Password = password;
            IpLocation = ipLocation;
        }
    }
}
