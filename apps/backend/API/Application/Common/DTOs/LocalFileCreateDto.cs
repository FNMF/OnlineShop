﻿using API.Domain.Enums;

namespace API.Application.Common.DTOs
{
    public class LocalFileCreateDto
    {
        public IFormFile File { get; }
        public byte[]? ObjectUuid { get; }
        public LocalfileObjectType LocalfileObjectType { get; }
        public byte[] UploderUuid { get; }
        public string UploderIp { get; }

        public LocalFileCreateDto(IFormFile file, byte[]? objectUuid, LocalfileObjectType localfileObjectType, byte[] uploderUuid, string uploderIp)
        {
            File = file;
            ObjectUuid = objectUuid;
            LocalfileObjectType = localfileObjectType;
            UploderUuid = uploderUuid;
            UploderIp = uploderIp;
        }
    }
}
