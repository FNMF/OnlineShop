using API.Api.Models;
using API.Application.DTOs;
using API.Application.Interfaces;
using API.Common.Helpers;
using API.Domain.Entities.Models;
using API.Domain.Enums;
using API.Domain.Interfaces;
using API.Infrastructure.Extensions;
using API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogService _logService;
        private readonly ILogger<AddressService> _logger;
        private readonly ICurrentService _currentService;
        public AddressService(IAddressRepository addressRepository, ILogService logService, ILogger<AddressService> logger, ICurrentService currentService)
        {
            _addressRepository = addressRepository;
            _logService = logService;
            _logger = logger;
            _currentService = currentService;
        }
        public async Task<List<Address>> GetAllAddressByUuid(AddressQueryOptions addressQueryOptions)        //通过uuid查询这个人所有的地址并返回一个地址List
        {
            try
            {
                if (addressQueryOptions == null)
                {
                    throw new ArgumentNullException(nameof(addressQueryOptions), "传入查询参数为空");
                }

                if (!_currentService.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException("未登录用户无法查询地址");
                }
                var query = _addressRepository.QueryAddresses();

                // 鉴权过滤
                if (_currentService.CurrentType == CurrentType.User)
                {
                    // 限定只查看自己的通知
                    if (_currentService.CurrentUuid != null)
                    {
                        query = query.Where(x => x.AddressUseruuid == _currentService.CurrentUuid&&x.AddressIsdeleted==false);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("未知用户");
                    }
                }
                else if (_currentService.CurrentType == CurrentType.Merchant)
                {
                    throw new UnauthorizedAccessException("商户不能查询用户地址");
                }
                else if (_currentService.CurrentType == CurrentType.System || _currentService.CurrentType == CurrentType.Platform)
                {
                    query = query.Where(x => x.AddressUseruuid == addressQueryOptions.UuidBytes);
                }
                // 排序分页
                query = query.OrderByDescending(x => x.AddressUuid)
                             .PageBy(addressQueryOptions.PageNumber, addressQueryOptions.PageSize);
                var addresses = await query.ToListAsync(); 

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
                            _logger.LogError(ex, "解码地址时出错，UserUuid:{uuid}", addressQueryOptions.UuidBytes);
                        }
                    }
                }

                return addresses ?? new List<Address>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取用户地址失败，UserUuid:{Uuid}", addressQueryOptions.UuidBytes);
                return new List<Address>();
            }
        }
        public async Task<bool> CreateAddressByUuid(AddressCreateOptions addressCreateOptions)       //通过传入的Dto和uuid创建新地址    
        {

            try
            {
                if (addressCreateOptions == null)
                {
                    _logger.LogWarning("创建地址时 DTO 为空");
                    return false;
                }
                var query = _addressRepository.QueryAddresses();
                // 鉴权过滤
                if (_currentService.CurrentType == CurrentType.User)
                {
                    // 限定只查看自己的通知
                    if (_currentService.CurrentUuid != null)
                    {
                        query = query.Where(x => x.AddressUseruuid == _currentService.CurrentUuid && x.AddressIsdeleted == false);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("未知用户");
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("只有用户能创建地址");
                }
                // 如果是默认地址，先将该用户之前的默认地址取消
                if (addressCreateOptions.IsDefault)
                {
                    query = query.Where(x =>x.AddressIsdefault == true);
                    var existingDefaults = await query.ToListAsync();
                    //var existingDefaults = await GetAllIsdefaultAsync(uuidBytes);

                    foreach (var addr in existingDefaults)
                    {
                        addr.AddressIsdefault = false;
                    }
                }

                var address = new Address
                {
                    AddressName = addressCreateOptions.Name,
                    AddressProvince = addressCreateOptions.Province,
                    AddressCity = addressCreateOptions.City,
                    AddressDistrict = addressCreateOptions.District,
                    AddressDetail = AESHelper.Encrypt(addressCreateOptions.Detail),
                    AddressPhone = AESHelper.Encrypt(addressCreateOptions.Phone),
                    AddressIsdefault = addressCreateOptions.IsDefault,
                    AddressTime = DateTime.Now,
                    AddressUseruuid = _currentService.CurrentUuid,
                    AddressUuid = UuidV7Helper.NewUuidV7ToBtyes()
                };


                await _addressRepository.AddAddressAsync(address);
                await _logService.AddLog(LogType.user, "用户创建地址", "无", _currentService.CurrentUuid, JsonSerializer.Serialize(address));

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建用户地址失败，UserUuid:{Uuid}", _currentService.CurrentUuid);
                return false;
            }
        }
        public async Task<bool?> DeleteAddressByUuid(AddressDeleteOptions addressDeleteOptions)
        {
            try
            {
                var query = _addressRepository.QueryAddresses();
                // 鉴权过滤
                if (_currentService.CurrentType == CurrentType.User)
                {
                    // 限定只查看自己的通知
                    if (_currentService.CurrentUuid != null)
                    {
                        query = query.Where(x => x.AddressUseruuid == _currentService.CurrentUuid && x.AddressIsdeleted == false && x.AddressUuid == addressDeleteOptions.AddressUuid);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("未知用户");
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("只有用户能更新地址");
                }
                var uuidBytes = addressDeleteOptions.AddressUuid;
                var address = await query.FirstOrDefaultAsync();
                //var address = await GetAddressByUuidAsync(uuidBytes);
                if (address == null)
                {
                    return false;
                }

                address.AddressIsdeleted = true;

                await _addressRepository.UpdateAddressAsync(address);
                await _logService.AddLog(LogType.user, "用户删除地址", "逻辑删除", uuidBytes, JsonSerializer.Serialize(address));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除地址失败，AddressUuid:{Uuid}", addressDeleteOptions.AddressUuid);
                return false;
            }
        }

        public async Task<bool> UpdateAddressByUuid(AddressUpdateOptions addressUpdateOptions)
        {
            try
            {
                if (addressUpdateOptions == null)
                {
                    _logger.LogWarning("修改地址时 DTO 为空");
                    return false;
                }
                var query = _addressRepository.QueryAddresses();
                // 鉴权过滤
                if (_currentService.CurrentType == CurrentType.User)
                {
                    // 限定只查看自己的通知
                    if (_currentService.CurrentUuid != null)
                    {
                        query = query.Where(x => x.AddressUseruuid == _currentService.CurrentUuid && x.AddressIsdeleted == false&&x.AddressUuid==addressUpdateOptions.AddressUuid);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("未知用户");
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("只有用户能更新地址");
                }
                var address = await query.FirstOrDefaultAsync();
                //var address = await GetAddressByUuidAsync(uuidBytes);

                if (address == null)
                {
                    _logger.LogWarning("修改地址时原地址不存在或异常");
                    return false;
                }


                if (addressUpdateOptions.IsDefault)
                {
                    query = _addressRepository.QueryAddresses();        //可能存在错误，使用了可能未重置的query
                    query = query.Where(x => x.AddressUseruuid == _currentService.CurrentUuid && x.AddressIsdeleted == false &&x.AddressIsdefault == true);
                    var existingDefaults = await query.ToListAsync();
                    
                    //var existingDefaults = await GetAllIsdefaultAsync(uuidBytes);

                    foreach (var addr in existingDefaults)
                    {
                        addr.AddressIsdefault = false;
                    }
                }

                address.AddressName = addressUpdateOptions.Name;
                address.AddressPhone = AESHelper.Encrypt(addressUpdateOptions.Phone);
                address.AddressProvince = addressUpdateOptions.Province;
                address.AddressCity = addressUpdateOptions.City;
                address.AddressDistrict = addressUpdateOptions.District;
                address.AddressDetail = AESHelper.Encrypt(addressUpdateOptions.Detail);
                address.AddressIsdefault = addressUpdateOptions.IsDefault;
                address.AddressTime = DateTime.Now;

                await _addressRepository.UpdateAddressAsync(address);
                await _logService.AddLog(LogType.user, "用户修改地址", "无", _currentService.CurrentUuid, JsonSerializer.Serialize(address));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新地址失败，AddressUuid:{Uuid}", _currentService.CurrentUuid);
                return false;
            }


        }
        
    }

}
