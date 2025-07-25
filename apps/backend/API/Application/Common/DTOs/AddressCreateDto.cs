﻿namespace API.Application.Common.DTOs
{
    public class AddressCreateDto
    {
        public string Name { get; }
        public string Phone { get; }
        public string Province { get; }
        public string City { get; }
        public string District { get; }
        public string Detail { get; }
        public byte[] UserUuid { get; }

        public AddressCreateDto(AddressCreateDto dto)
        {
            Name = dto.Name;
            Phone = dto.Phone;
            Province = dto.Province;
            City = dto.City;
            District = dto.District;
            Detail = dto.Detail;
            UserUuid = dto.UserUuid;
        }

    }
}
