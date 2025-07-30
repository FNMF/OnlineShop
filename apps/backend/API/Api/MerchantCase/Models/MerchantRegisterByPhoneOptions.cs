namespace API.Api.MerchantCase.Models
{
    public class MerchantRegisterByPhoneOptions
    {
        public string Phone {  get; set; }
        public string Password { get; set; }
        public string ValidationCode { get; set; }
    }
}
