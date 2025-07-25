using API.Api.Common.Models;
using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;

namespace API.Domain.Services.AddressPart.Implementations
{
    public class AddressCreateService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressCreateService> _logger;

        public AddressCreateService(IAddressRepository addressRepository, ILogger<AddressCreateService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<Result<Address>> AddAddressAsync(AddressCreateDto dto)
        {
            try
            {
                var result = AddressFactory.Create(dto);
                if (!result.IsSuccess)
                {
                    return Result<Address>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _addressRepository.AddAddressAsync(result.Data);

                return result;
            }catch (Exception ex)
            {
                return Result<Address>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
