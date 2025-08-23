using API.Api.Common.Models;
using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.AddressPart.Interfaces;
using API.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Services.AddressPart.Implementations
{
    public class AddressReadService:IAddressReadService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressReadService> _logger;

        public AddressReadService(IAddressRepository addressRepository, ILogger<AddressReadService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<Result<List<Address>>> GetAllAddresses(AddressQueryOptions opt)
        {
            try
            {
                var query = _addressRepository.QueryAddresses();

                query = query
                    .WhereIfNotNull(opt.Uuid, u => u.AddressUseruuid == opt.Uuid)
                    .WhereIfNotNull(opt.IsDeleted, u => u.AddressIsdeleted == opt.IsDeleted)
                    .OrderByDescending(u => u.AddressTime)
                    .PageBy(opt.PageNumber, opt.PageSize);

                if(!query.Any())
                {
                    return Result<List<Address>>.Fail(ResultCode.NotFound, "地址不存在或已删除");
                }

                var result = Result<List<Address>>.Success(await query.ToListAsync());

                return result;
            }catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<List<Address>>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }

        public async Task<Result<Address>> GetAddress(Guid addressUuid)
        {
            try
            {
                var query = _addressRepository.QueryAddresses();

                var address = await query
                    .FirstOrDefaultAsync(u => addressUuid == u.AddressUseruuid);

                if ( address == null )
                {
                    return Result<Address>.Fail(ResultCode.NotFound, "地址不存在或已删除");
                }

                var result = Result<Address>.Success(address);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<Address>.Fail(ResultCode.ServerError, "服务器错误");
            }
        }
    }
}
