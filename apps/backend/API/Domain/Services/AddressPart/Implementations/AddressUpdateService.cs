using API.Application.Common.DTOs;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.AddressPart.Interfaces;

namespace API.Domain.Services.AddressPart.Implementations
{
    public class AddressUpdateService:IAddressUpdateService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressUpdateService> _logger;

        public AddressUpdateService(IAddressRepository addressRepository, ILogger<AddressUpdateService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<Result<Address>> UpdateAddressAsync(AddressUpdateDto dto)
        {
            try
            {
                var result = AddressFactory.Update(dto);
                if (!result.IsSuccess)
                {
                    return Result<Address>.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                await _addressRepository.UpdateAddressAsync(result.Data);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "服务器错误");
                return Result<Address>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
