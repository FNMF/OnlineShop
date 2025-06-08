using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using API.Database;
using API.Entities.Dto;
using API.Entities.Models;

namespace API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly OnlineshopContext _context;

        public AddressRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public async Task<List<Address>> GetAllAddressByUuidAsync(byte[] uuidBytes)        
        {
                return await _context.Addresses
                    .Where(a => a.AddressUseruuid == uuidBytes&&a.AddressIsdeleted ==false)
                    .ToListAsync();

        }
        public async Task<List<Address>> GetAllIsdefaultAsync(byte[] uuidBytes)
        {
            return await _context.Addresses
                .Where(a => a.AddressUseruuid == uuidBytes && a.AddressIsdefault == true && a.AddressIsdeleted == false)
                        .ToListAsync();
        }
        public async Task<Address> GetAddressByUuidAsync(byte[] uuidBytes)
        {
           return await _context.Addresses
                    .FirstOrDefaultAsync(a => a.AddressUuid == uuidBytes && a.AddressIsdeleted == false);
        }
        public async Task<bool> CreateAddressAsync(Address address)      
        {
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
                return true;
        }
        
        public async Task<bool> UpdateAddressAsync(Address address)        
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
