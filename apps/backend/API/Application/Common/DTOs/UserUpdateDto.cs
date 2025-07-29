namespace API.Application.Common.DTOs
{
    public class UserUpdateDto
    {
        public string Name { get; }
        public string OpenId { get; }
        public string Phone {  get; }
        public int BpChange {  get; }
        public int CreditChange { get; }
        public UserUpdateDto(string name, string openId,string phone,int bpChange,int creditChange)
        {
            Name = name;
            OpenId = openId;
            Phone = phone;
            BpChange = bpChange;
            CreditChange = creditChange;
        }
    }
}
