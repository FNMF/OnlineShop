﻿using API.Application.Common.DTOs;
using API.Common.Helpers;
using API.Common.Interfaces;
using API.Domain.Enums;
using API.Domain.Interfaces;
using System.Text.Json;

namespace API.Application.Services
{
    public class AdminService
    {
        private readonly IAdminRepository _repository;
        private readonly ILogService _logService;
        private readonly ILogger<AdminService> _logger;

        public AdminService(IAdminRepository repository, ILogService logService, ILogger<AdminService> logger)
        {
            _repository = repository;
            _logService = logService;
            _logger = logger;
        }
        public async Task<RAdminDto> GetAdminByUuid(Guid uuid)
        {
            try
            {
                byte[] uuidBytes = uuid.ToByteArray();
                var admin = await _repository.GetAdminByUuidAsync(uuidBytes);
                if (admin == null) { _logger.LogWarning("管理员不存在"); return null; }
                var radmin = new RAdminDto
                {
                    phone = AESHelper.Decrypt(admin.AdminPhone),
                    account = admin.AdminAccount
                };
                return radmin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取管理员信息时出错,AdminUuid:{Uuid}", uuid);
                return null;
            }
        }
        public async Task<RAdminDto> GetAdminByAccount(int account)
        {
            try
            {
                var admin = await _repository.GetAdminByAccountAsync(account);
                if (admin == null) { _logger.LogWarning("管理员不存在"); return null; }
                var radmin = new RAdminDto
                {
                    phone = AESHelper.Decrypt(admin.AdminPhone),
                    account = admin.AdminAccount
                };
                return radmin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取管理员信息时出错,AdminAccount:{Account}", account);
                return null;
            }
        }
        public async Task<RAdminDto> UpdateAdmin(CUAdminDto dto, Guid uuid)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogWarning("修改管理员时 DTO 为空");
                    return null;
                }
                byte[] uuidBytes = uuid.ToByteArray();
                var admin = await _repository.GetAdminByUuidAsync(uuidBytes);

                if (admin == null)
                {
                    _logger.LogWarning("修改管理员时原管理员不存在或异常");
                    return null;
                }
                admin.AdminPhone = AESHelper.Encrypt(dto.phone);
                await _repository.UpdateAdminAsync(admin);
                await _logService.AddLog(LogType.admin, "修改管理员信息", "无", uuidBytes, JsonSerializer.Serialize(dto));
                var radmin = new RAdminDto
                {
                    account = admin.AdminAccount,
                    phone = AESHelper.Decrypt(admin.AdminPhone)

                };
                return radmin;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新管理员状态时出错");
                return null;
            }
        }
        public async Task<bool> DeleteAdminByUuid(Guid uuid)
        {
            try
            {

                byte[] uuidBytes = uuid.ToByteArray();
                var admin = await _repository.GetAdminByUuidAsync(uuidBytes);

                if (admin == null)
                {
                    _logger.LogWarning("删除管理员时原管理员不存在或异常");
                    return false;
                }
                await _repository.UpdateAdminAsync(admin);
                await _logService.AddLog(LogType.admin, "删除管理员信息", "逻辑删除", uuidBytes, JsonSerializer.Serialize(admin));

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新管理员状态时出错");
                return false;
            }
        }
        /*public async Task<RAdminDto> CreateAdmin(CUAdminDto dto)
        {
            try
            {
                var admin = new Admin { AdminLastlocation = dto.ipaddress,
                    AdminLastlogintime = DateTime.Now,
                };
            }
        }*/
    }
}
