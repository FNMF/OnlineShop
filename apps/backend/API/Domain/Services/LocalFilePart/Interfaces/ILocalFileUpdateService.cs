using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.LocalFilePart.Interfaces
{
    public interface ILocalFileUpdateService
    {
        Task<Result<Localfile>> UpdateLocalFileAsync(LocalFileUpdateDto dto);
        Task<Result<List<Localfile>>> UpdateBatchLocalFilesAsync(List<LocalFileUpdateDto> dtos);
        Task<Result> MarkAsDeleteAsync(Localfile localfile);
        Task<Result> BatchMarkAsDeleteAsync(List<Localfile> localfiles);
    }
}
