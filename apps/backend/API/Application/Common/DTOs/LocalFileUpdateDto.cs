using API.Domain.Enums;

namespace API.Application.Common.DTOs
{
    public class LocalFileUpdateDto
    {
        public IFormFile File { get; }
        public byte[] UploderUuid { get; }
        public string UploderIp { get; }
        public byte[] LocalFileUuid { get; }
        public byte[] ObjectUuid { get; } 
        public LocalfileObjectType ObjectType { get; }

        public LocalFileUpdateDto(IFormFile file, byte[] uploderUuid, string uploderIp, byte[] localFileUuid, byte[] objectUuid, LocalfileObjectType objectType)
        {
            File = file;
            UploderUuid = uploderUuid;
            UploderIp = uploderIp;
            LocalFileUuid = localFileUuid;
            ObjectUuid = objectUuid;
            ObjectType = objectType;
        }
    }
}
