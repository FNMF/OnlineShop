namespace API.Application.Common.DTOs
{
    public class WxLoginDto
    {
        public string Code { get; set; }
        public string EncryptedData { get; set; }
        public string Iv { get; set; }
        public string EncryptedPhoneData { get; set; }
        public string PhoneIv { get; set; }
    }
}
