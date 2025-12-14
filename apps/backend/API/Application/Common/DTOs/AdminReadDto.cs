namespace API.Application.Common.DTOs
{
    public class AdminReadDto
    {
        public string Phone { get; }
        public Guid Uuid { get; }
        public int Account {  get; }

        public AdminReadDto(string phone, Guid uuid, int account)
        {
            Phone = phone;
            Uuid = uuid;
            Account = account;
        }
    }
}
