using DAL.Database;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<IdentityUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
