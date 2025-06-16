using API.Domain.Entities.Dto;
using API.Entities.Models;
using API.Repositories;
using System.Text.Json;

namespace API.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly ILogger<MerchantService> _logger;
        private readonly IMerchantRepository _merchantRepository;
        private readonly ILogService _logService;

        public MerchantService(ILogger<MerchantService> logger, IMerchantRepository merchantRepository, ILogService logService)
        {
            _logger = logger;
            _merchantRepository = merchantRepository;
            _logService = logService;
        }

        public async Task<List<Merchant>> GetMerchantsByLocationAsync(string province, string city)
        {
            try
            {
                var merchants = await _merchantRepository.GetMerchantsByLocation(province, city);
                return merchants ?? new List<Merchant>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取商户失败");
                return new List<Merchant>();
            }
        }
        public async Task<List<Merchant>> GetMerchantBySearchAsync(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search) || search.Length <= 3)
                {
                    return new List<Merchant>();
                }
                var merchants = await _merchantRepository.FuzzySearchMerchants(search);
                return merchants ?? new List<Merchant>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取商户失败");
                return new List<Merchant>();
            }
        }
        public async Task<Merchant> GetMerchantByAdminUuidAsync(Guid uuid)
        {
            try
            {
                byte[] uuidBytes = uuid.ToByteArray();
                var merchant = await _merchantRepository.GetMerchantByAdminUuid(uuidBytes);
                return merchant ?? new Merchant();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取商户失败");
                return new Merchant();
            }
        }
        public async Task<Merchant> GetMerchantByUuidAsync(Guid uuid)
        {
            try
            {
                byte[] uuidBytes = uuid.ToByteArray();
                var merchant = await _merchantRepository.GetMerchantByUuid(uuidBytes);
                return merchant ?? new Merchant();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取商户失败");
                return new Merchant();
            }
        }
        public async Task<Merchant> CreateMerchant(CUMerchantDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogWarning("创建商户时DTO为空");
                    return new Merchant();
                }
                var merchant = new Merchant
                {
                    MerchantProvince = dto.Province,
                    MerchantCity = dto.City,
                    MerchantDistrict = dto.District,
                    MerchantDetail = dto.Detail,
                    MerchantBusinessstart = dto.StartTime,
                    MerchantBusinessend = dto.EndTime,
                    MerchantName = dto.Name,
                    MerchantAdminuuid = dto.AdminUuid.ToByteArray(),
                    MerchantUuid = Guid.NewGuid().ToByteArray()
                };

                await _merchantRepository.AddMerchant(merchant);
                await _logService.AddLog("merchant", "商户创建", "无", dto.AdminUuid.ToByteArray(), JsonSerializer.Serialize(merchant));
                return merchant;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建商户时失败,MerchantUuid:{Uuid}", dto.AdminUuid.ToByteArray());
                return new Merchant();
            }
        }
        public async Task<Merchant> UpdateMerchant(CUMerchantDto dto, Guid uuid)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogWarning("修改商户时DTO为空");
                    return new Merchant();
                }
                byte[] uuidBytes = uuid.ToByteArray();
                var merchant = await _merchantRepository.GetMerchantByUuid(uuidBytes);

                if (merchant == null)
                {
                    _logger.LogWarning("修改商户时原商户不存在或异常");
                    return new Merchant();
                }
                merchant.MerchantProvince = dto.Province;
                merchant.MerchantName = dto.Name;
                merchant.MerchantCity = dto.City;
                merchant.MerchantDistrict = dto.District;
                merchant.MerchantDetail = dto.Detail;
                merchant.MerchantBusinessstart = dto.StartTime;
                merchant.MerchantBusinessend = dto.EndTime;

                await _merchantRepository.UpdateMerchant(merchant);
                await _logService.AddLog("merchant", "修改商户", "无", uuidBytes, JsonSerializer.Serialize(dto));
                return new Merchant();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "修改商户时失败,MerchantUuid:{Uuid}", uuid);
                return new Merchant();
            }
        }
        public async Task<bool?> DeleteMerchant(Guid uuid)
        {
            try
            {
                byte[] uuidBytes = uuid.ToByteArray();
                var merchant = await _merchantRepository.GetMerchantByUuid(uuidBytes);
                if (merchant == null)
                {
                    _logger.LogWarning("删除商户时商户不存在或异常");
                    return false;
                }
                merchant.MerchantIsdeleted = true;

                await _merchantRepository.UpdateMerchant(merchant);
                await _logService.AddLog("merchant", "删除商户", "逻辑删除", uuidBytes, JsonSerializer.Serialize(merchant));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除商户失败，MerchantUuid:{Uuid}", uuid);
                return false;
            }
        }
    }
}
