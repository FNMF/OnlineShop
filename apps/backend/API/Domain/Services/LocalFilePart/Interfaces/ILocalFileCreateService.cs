using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.LocalFilePart.Interfaces
{
    public interface ILocalFileCreateService
    {

        Task<Result<Localfile>> AddLocalFileAsync(LocalFileCreateDto dto);
        Task<Result<List<Localfile>>> AddBatchLocalFilesAsync(List<LocalFileCreateDto> dtos);
    }
}
