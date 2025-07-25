using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;

namespace API.Domain.Services.CartPart.Interfaces
{
    public interface ICartCreateService
    {
        Task<Result<Cart>> CreateCartAsync(CartCreateDto dto);
    }
}
