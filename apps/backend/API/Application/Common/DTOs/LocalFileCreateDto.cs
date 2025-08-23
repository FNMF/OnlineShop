using API.Domain.Enums;

namespace API.Application.Common.DTOs
{
    public class LocalFileCreateDto
    {
        public IFormFile File { get; }
        public Guid? ObjectUuid { get; }
        public LocalfileObjectType LocalfileObjectType { get; }
        public Guid UploderUuid { get; }
        public string UploderIp { get; }
        public int SortNumber { get; }

        public LocalFileCreateDto(IFormFile file, Guid? objectUuid, LocalfileObjectType localfileObjectType, Guid uploderUuid, string uploderIp, int sortNumber)
        {
            File = file;
            ObjectUuid = objectUuid;
            LocalfileObjectType = localfileObjectType;
            UploderUuid = uploderUuid;
            UploderIp = uploderIp;
            SortNumber = sortNumber;
        }
    }
}
