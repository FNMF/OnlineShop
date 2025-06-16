using API.Domain.Entities.Models;
using API.Entities.Dto;
using API.Helpers;
using API.Repositories;
using System.Text.Json;

namespace API.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogService _logService;
        private readonly ILogger<AddressService> _logger;
        public AddressService(IAddressRepository addressRepository, ILogService logService, ILogger<AddressService> logger)
        {
            _addressRepository = addressRepository;
            _logService = logService;
            _logger = logger;
        }
        public async Task<List<Address>> GetAllAddressByUuidAsync(Guid useruuid)        //通过uuid查询这个人所有的地址并返回一个地址List
        {
            try
            {
                var uuidBytes = useruuid.ToByteArray();

                var addresses = await _addressRepository.GetAllAddressByUuidAsync(uuidBytes);

                if (addresses != null)      //遍历解码所有的加密的敏感信息
                {
                    foreach (var address in addresses)
                    {
                        try
                        {
                            address.AddressPhone = AESHelper.Decrypt(address.AddressPhone);
                            address.AddressDetail = AESHelper.Decrypt(address.AddressDetail);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "解码地址时出错，UserUuid:{uuid}", useruuid);
                        }
                    }
                }

                return addresses ?? new List<Address>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取用户地址失败，UserUuid:{Uuid}", useruuid);
                return new List<Address>();
            }
        }
        public async Task<bool> CreateAddressByUuidAsync(CURAddressDto dto, Guid useruuid)       //通过传入的Dto和uuid创建新地址    
        {

            try
            {
                if (dto == null)
                {
                    _logger.LogWarning("创建地址时 DTO 为空");
                    return false;
                }

                var uuidBytes = useruuid.ToByteArray();

                // 如果是默认地址，先将该用户之前的默认地址取消
                if (dto.IsDefault)
                {
                    var existingDefaults = await _addressRepository.GetAllIsdefaultAsync(uuidBytes);

                    foreach (var addr in existingDefaults)
                    {
                        addr.AddressIsdefault = false;
                    }
                }

                var address = new Address
                {
                    AddressName = dto.Name,
                    AddressProvince = dto.Province,
                    AddressCity = dto.City,
                    AddressDistrict = dto.District,
                    AddressDetail = AESHelper.Encrypt(dto.Detail),
                    AddressPhone = AESHelper.Encrypt(dto.Phone),
                    AddressIsdefault = dto.IsDefault,
                    AddressTime = DateTime.Now,
                    AddressUseruuid = uuidBytes,
                    AddressUuid = Guid.NewGuid().ToByteArray()
                };


                await _addressRepository.AddAddressAsync(address);
                await _logService.AddLog("user", "用户创建地址", "无", uuidBytes, JsonSerializer.Serialize(address));

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建用户地址失败，UserUuid:{Uuid}", useruuid);
                return false;
            }
        }
        public async Task<bool?> DeleteAddressByUuidAsync(Guid addressuuid)
        {
            try
            {
                var uuidBytes = addressuuid.ToByteArray();

                var address = await _addressRepository.GetAddressByUuidAsync(uuidBytes);
                if (address == null)
                {
                    return false;
                }

                address.AddressIsdeleted = true;

                await _addressRepository.UpdateAddressAsync(address);
                await _logService.AddLog("user", "用户删除地址", "逻辑删除", uuidBytes, JsonSerializer.Serialize(address));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除地址失败，AddressUuid:{Uuid}", addressuuid);
                return false;
            }
        }

        public async Task<bool> UpdateAddressByUuidAsync(Guid addressuuid, CURAddressDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogWarning("修改地址时 DTO 为空");
                    return false;
                }
                var uuidBytes = addressuuid.ToByteArray();
                var address = await _addressRepository.GetAddressByUuidAsync(uuidBytes);

                if (address == null)
                {
                    _logger.LogWarning("修改地址时原地址不存在或异常");
                    return false;
                }


                if (dto.IsDefault)
                {
                    var existingDefaults = await _addressRepository.GetAllIsdefaultAsync(uuidBytes);

                    foreach (var addr in existingDefaults)
                    {
                        addr.AddressIsdefault = false;
                    }
                }

                address.AddressName = dto.Name;
                address.AddressPhone = AESHelper.Encrypt(dto.Phone);
                address.AddressProvince = dto.Province;
                address.AddressCity = dto.City;
                address.AddressDistrict = dto.District;
                address.AddressDetail = AESHelper.Encrypt(dto.Detail);
                address.AddressIsdefault = dto.IsDefault;
                address.AddressTime = DateTime.Now;

                await _addressRepository.UpdateAddressAsync(address);
                await _logService.AddLog("user", "用户修改地址", "无", uuidBytes, JsonSerializer.Serialize(address));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新地址失败，AddressUuid:{Uuid}", addressuuid);
                return false;
            }


        }
    }

}
