using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;

namespace API.Domain.Services.AddressPart.Implementations
{
    public class AddressRemoveService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressRemoveService> _logger;

        public AddressRemoveService(IAddressRepository addressRepository, ILogger<AddressRemoveService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<Result> RemoveAddressAsync(byte[] addressUuid)
        {
            try
            {
                if (addressUuid == null)
                {
                    return Result.Fail(ResultCode.ValidationError, "输入数据不合法");
                }

                var query = _addressRepository.QueryAddresses();

                var address = query.FirstOrDefault(a =>a.AddressUuid == addressUuid && a.AddressIsdeleted == false);

                if (address == null)
                {
                    return Result.Fail(ResultCode.NotFound, "地址不存在或已删除");
                }

                address.AddressIsdeleted = true;
                await _addressRepository.UpdateAddressAsync(address);

                return Result.Success();

            }catch (Exception ex) 
            {
                return Result.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
