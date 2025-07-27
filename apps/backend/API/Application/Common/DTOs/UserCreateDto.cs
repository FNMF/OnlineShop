namespace API.Application.Common.DTOs
{
    public class UserCreateDto
    {
        public string Name { get; }
        public string OpenId { get; }
        public string Phone { get; }
        public string AvatarUrl { get; }

        public UserCreateDto(string name,string openId,string phone,string avatarUrl)
        {
            Name = name;
            OpenId = openId;
            Phone = phone;
            AvatarUrl = avatarUrl;
        }
    }
}
