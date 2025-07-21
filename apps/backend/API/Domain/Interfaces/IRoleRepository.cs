using API.Domain.Entities.Models;

namespace API.Domain.Interfaces
{
    public interface IRoleRepository
    {
        public IQueryable<Role> QueryRoles();
    }
}
