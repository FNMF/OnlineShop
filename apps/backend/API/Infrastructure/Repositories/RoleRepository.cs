using API.Domain.Entities.Models;
using API.Domain.Interfaces;
using API.Infrastructure.Database;

namespace API.Infrastructure.Repositories
{
    public class RoleRepository:IRoleRepository
    {
        private readonly OnlineshopContext _context;
        public RoleRepository(OnlineshopContext context)
        {
            _context = context;
        }
        public IQueryable<Role> QueryRoles() 
        {
            return _context.Roles;
        }
    }
}
