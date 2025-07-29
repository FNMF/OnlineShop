<<<<<<< HEAD
﻿using API.Common.Models.Results;
using API.Domain.Entities.Models;
using API.Domain.Interfaces;

namespace API.Domain.Services.AddressPart.Implementations
{
    public class ProductReadService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<ProductReadService> _logger;

        public ProductReadService(IAddressRepository addressRepository, ILogger<ProductReadService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<Result<List<Address>>> GetAllAddress()
        {
            return null;
        }
=======
﻿using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Domain.Services.ProductPart.Interfaces;

namespace API.Domain.Services.ProductPart.Implementations
{
    public class ProductReadService:IProductReadService
    {
        /*public readonly IProductRepository _ProductRepository;*/
        public readonly ILogger<ProductReadService> _Logger;

        public ProductReadService(/*IProductRepository productRepository,*/ ILogger<ProductReadService> logger)
        {
            /*_ProductRepository = productRepository;*/
            _Logger = logger;
        }
        /*      这一块需要太多种不同的查询，暂时放置一会
        public async Task<List<Product>> GetProduct
        */
>>>>>>> af48421247986fcc6eddeb2646351f5243e74c57
    }
}
