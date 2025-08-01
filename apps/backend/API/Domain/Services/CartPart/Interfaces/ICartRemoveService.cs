﻿using API.Common.Models.Results;

namespace API.Domain.Services.CartPart.Interfaces
{
    public interface ICartRemoveService
    {
        Task<Result> RemoveCartAsync(byte[] cartUuid);
    }
}
