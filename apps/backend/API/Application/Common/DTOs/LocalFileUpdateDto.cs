using API.Domain.Enums;

namespace API.Application.Common.DTOs
{
    public class LocalFileUpdateDto
    {
        public IFormFile File { get; }
        public Guid UploderUuid { get; }
        public string UploderIp { get; }
        public Guid LocalFileUuid { get; }
        public Guid ObjectUuid { get; } 
        public LocalfileObjectType ObjectType { get; }
        public int SortNumber { get; }

        public LocalFileUpdateDto(IFormFile file, Guid uploderUuid, string uploderIp, Guid localFileUuid, Guid objectUuid, LocalfileObjectType objectType, int sortNumber)
        {
            File = file;
            UploderUuid = uploderUuid;
            UploderIp = uploderIp;
            LocalFileUuid = localFileUuid;
            ObjectUuid = objectUuid;
            ObjectType = objectType;
            SortNumber = sortNumber;
        }
    }
}
