using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Common.Helpers;
using API.Application.Common.DTOs;

namespace API.Domain.Services.LocalFilePart
{
    //todo
    //这里后续要添加对象存储支持，比如AWS S3，阿里云OSS等
    public class LocalFileFactory: ILocalFileFactory
    {
        private static readonly string Root = Environment.GetEnvironmentVariable("WEB_ROOT")
        ?? throw new Exception("WEB_ROOT not found");
        private static readonly long MaxImageSize = 5 * 1024 * 1024;   // 5MB
        private static readonly long MaxFileSize = 10 * 1024 * 1024;   // 10MB

        private static readonly string[] AllowedImageTypes =
        {
    "image/jpeg",
    "image/png",
    "image/webp"
};


        public async Task<Result<Localfile>> Create(LocalFileCreateDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return Result<Localfile>.Fail(ResultCode.ValidationError, "文件为空");

            // 基本大小校验
            if (dto.File.ContentType.StartsWith("image"))
            {
                if (!AllowedImageTypes.Contains(dto.File.ContentType))
                    return Result<Localfile>.Fail(ResultCode.ValidationError, "不支持的图片格式");

                if (dto.File.Length > MaxImageSize)
                    return Result<Localfile>.Fail(ResultCode.ValidationError, "图片大小不能超过 5MB");
            }
            else
            {
                if (dto.File.Length > MaxFileSize)
                    return Result<Localfile>.Fail(ResultCode.ValidationError, "文件大小不能超过 10MB");
            }

            var uploadPath = Path.Combine(Root, "uploads");
            Directory.CreateDirectory(uploadPath);

            var fileExt = Path.GetExtension(dto.File.FileName);
            var newFileName = Guid.NewGuid() + fileExt;
            var filePath = Path.Combine(uploadPath, newFileName);
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            var fileInfo = new FileInfo(filePath);

            var localFile = new Localfile
            {
                Name = dto.File.FileName,
                Path = $"/uploads/{newFileName}",
                Size = fileInfo.Length,
                LocalfileType = dto.File.ContentType,
                CreatedAt = DateTime.Now,
                Uuid = UuidV7Helper.NewUuidV7(),
                ObjectUuid = dto.ObjectUuid,
                LocalfileObjectType = dto.LocalfileObjectType.ToString().ToLowerInvariant(),
                UploaderUuid = dto.UploderUuid,
                UploadIp = dto.UploderIp,
                IsAudited = false,
                IsDeleted = false,
                MimeType = dto.File.ContentType
            };

            return Result<Localfile>.Success(localFile);
        }

        //不推荐直接Update方法，而是通过软删除加新增来替代Update
        public async Task<Result<Localfile>> Update(LocalFileUpdateDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return Result<Localfile>.Fail(ResultCode.ValidationError, "文件为空");

            // 基本大小校验
            if (dto.File.ContentType.StartsWith("image"))
            {
                if (!AllowedImageTypes.Contains(dto.File.ContentType))
                    return Result<Localfile>.Fail(ResultCode.ValidationError, "不支持的图片格式");

                if (dto.File.Length > MaxImageSize)
                    return Result<Localfile>.Fail(ResultCode.ValidationError, "图片大小不能超过 5MB");
            }
            else
            {
                if (dto.File.Length > MaxFileSize)
                    return Result<Localfile>.Fail(ResultCode.ValidationError, "文件大小不能超过 10MB");
            }

            var uploadPath = Path.Combine(Root, "uploads");
            Directory.CreateDirectory(uploadPath);

            var fileExt = Path.GetExtension(dto.File.FileName);
            var newFileName = Guid.NewGuid() + fileExt;
            var filePath = Path.Combine(uploadPath, newFileName);
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            var fileInfo = new FileInfo(filePath);

            var localFile = new Localfile
            {
                Name = dto.File.FileName,
                Path = $"/uploads/{newFileName}",
                Size = fileInfo.Length,
                LocalfileType = dto.File.ContentType,
                CreatedAt = DateTime.Now,
                Uuid = dto.LocalFileUuid,
                UploaderUuid = dto.UploderUuid,
                UploadIp = dto.UploderIp,
                IsAudited = false,
                IsDeleted = false,
                MimeType = dto.File.ContentType,

            };

            return Result<Localfile>.Success(localFile);

        }
    }
}
