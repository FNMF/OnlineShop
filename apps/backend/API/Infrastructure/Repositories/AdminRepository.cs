using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly OnlineshopContext _context;
        public AdminRepository(OnlineshopContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetAdminByAccountAsync(int account)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.AdminAccount == account && a.AdminIsdeleted == false);
        }
        public async Task<Admin> GetAdminByUuidAsync(byte[] uuidBytes)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.AdminUuid == uuidBytes && a.AdminIsdeleted == false);
        }
        public async Task<Admin> GetAdminByPhoneAsync(string phone)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.AdminPhone == phone && a.AdminIsdeleted == false);
        }
        public async Task<Admin> AddAdminAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
        public async Task<Admin> UpdateAdminAsync(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
    }
}
