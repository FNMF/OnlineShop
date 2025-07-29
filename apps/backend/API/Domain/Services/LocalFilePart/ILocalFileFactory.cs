using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.LocalFilePart
{
    public interface ILocalFileFactory
    {
        Task<Result<Localfile>> Create(LocalFileCreateDto dto);
        Task<Result<Localfile>> Update(LocalFileUpdateDto dto);
    }
}
