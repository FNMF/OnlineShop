namespace API.Application.Common.DTOs
{
    public class AdminReadDto
    {
        public string Phone { get; }
        public string Key { get; }
        public Guid Uuid { get; }
        public int Account {  get; }

        public AdminReadDto(string phone, string key, Guid uuid, int account)
        {
            Phone = phone;
            Key = key;
            Uuid = uuid;
            Account = account;
        }
    }
}
