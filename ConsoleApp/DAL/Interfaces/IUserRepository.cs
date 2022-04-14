using Microsoft.AspNetCore.Identity;

namespace DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<IdentityUser>
    {
    }
}
