using API.Common.Models.Results;
using API.Domain.Entities.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using Image = SixLabors.ImageSharp.Image;
using API.Common.Helpers;
using API.Application.Common.DTOs;

namespace API.Domain.Services.LocalFilePart
{
    public class LocalFileFactory: ILocalFileFactory
    {
        private static readonly string Root = Environment.GetEnvironmentVariable("WEB_ROOT")
        ?? throw new Exception("WEB_ROOT not found");

        public async Task<Result<Localfile>> Create(LocalFileCreateDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return Result<Localfile>.Fail(ResultCode.ValidationError, "文件为空");

            var uploadPath = Path.Combine(Root, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileExt = Path.GetExtension(dto.File.FileName);
            var newFileName = Guid.NewGuid().ToString() + fileExt;
            var filePath = Path.Combine(uploadPath, newFileName);

            // 压缩逻辑（仅对图片处理）
            if (dto.File.ContentType.StartsWith("image"))
            {
                using var image = await Image.LoadAsync(dto.File.OpenReadStream());

                // 例如：压缩到 80% 质量
                var encoder = new JpegEncoder { Quality = 80 };

                // 可选：调整尺寸（防止超大图）
                if (image.Width > 1920)
                {
                    image.Mutate(x => x.Resize(1920, 0)); // 按宽等比缩放
                }

                await image.SaveAsync(filePath, encoder);
            }
            else
            {
                // 非图片直接保存
                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.File.CopyToAsync(stream);
            }
            var fileInfo = new FileInfo(filePath);

            var localFile = new Localfile
            {
                LocalfileName = dto.File.FileName,
                LocalfilePath = $"/uploads/{newFileName}",
                LocalfileSize = fileInfo.Length,
                LocalfileType = dto.File.ContentType,
                LocalfileCreatedat = DateTime.Now,
                LocalfileUuid = UuidV7Helper.NewUuidV7ToBtyes(),
                LocalfileObjectuuid = dto.ObjectUuid,
                LocalfileObjecttype = dto.LocalfileObjectType.ToString().ToLowerInvariant(),
                LocalfileUploaderuuid = dto.UploderUuid,
                LocalfileUploadip = dto.UploderIp,
                LocalfileIsaudited = false,
                LocalfileIsdeleted = false,
                LocalfileMimetype = dto.File.ContentType,

            };

            return Result<Localfile>.Success(localFile);

        }

        public async Task<Result<Localfile>> Update(LocalFileUpdateDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return Result<Localfile>.Fail(ResultCode.ValidationError, "文件为空");

            var uploadPath = Path.Combine(Root, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileExt = Path.GetExtension(dto.File.FileName);
            var newFileName = Guid.NewGuid().ToString() + fileExt;
            var filePath = Path.Combine(uploadPath, newFileName);

            // 压缩逻辑（仅对图片处理）
            if (dto.File.ContentType.StartsWith("image"))
            {
                using var image = await Image.LoadAsync(dto.File.OpenReadStream());

                // 例如：压缩到 80% 质量
                var encoder = new JpegEncoder { Quality = 80 };

                // 可选：调整尺寸（防止超大图）
                if (image.Width > 1920)
                {
                    image.Mutate(x => x.Resize(1920, 0)); // 按宽等比缩放
                }

                await image.SaveAsync(filePath, encoder);
            }
            else
            {
                // 非图片直接保存
                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.File.CopyToAsync(stream);
            }
            var fileInfo = new FileInfo(filePath);

            var localFile = new Localfile
            {
                LocalfileName = dto.File.FileName,
                LocalfilePath = $"/uploads/{newFileName}",
                LocalfileSize = fileInfo.Length,
                LocalfileType = dto.File.ContentType,
                LocalfileCreatedat = DateTime.Now,
                LocalfileUuid = dto.LocalFileUuid,
                LocalfileUploaderuuid = dto.UploderUuid,
                LocalfileUploadip = dto.UploderIp,
                LocalfileIsaudited = false,
                LocalfileIsdeleted = false,
                LocalfileMimetype = dto.File.ContentType,

            };

            return Result<Localfile>.Success(localFile);

        }
    }
}
