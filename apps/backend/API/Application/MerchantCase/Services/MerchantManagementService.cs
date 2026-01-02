using API.Application.MerchantCase.Interfaces;
using API.Common.Interfaces;
using API.Common.Models.Results;
using API.Domain.Services.MerchantPart.Interfaces;

namespace API.Application.MerchantCase.Services
{
    public class MerchantManagementService:IMerchantManagementService
    {
        private readonly IMerchantCreateService _merchantCreateService;
        private readonly IMerchantReadService _merchantReadService;
        private readonly IMerchantUpdateService _merchantUpdateService;
        private readonly ICurrentService _currentService;
        private readonly ILogger<MerchantManagementService> _logger;

        public MerchantManagementService(
            IMerchantCreateService merchantCreateService,
            IMerchantReadService merchantReadService,
            IMerchantUpdateService merchantUpdateService,
            ICurrentService currentService,
            ILogger<MerchantManagementService> logger)
        {
            _merchantCreateService = merchantCreateService;
            _merchantReadService = merchantReadService;
            _merchantUpdateService = merchantUpdateService;
            _currentService = currentService;
            _logger = logger;
        }
        
        /*public async Task<Result<>>*/
    }
}
