using DAL.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserByUserCredentialsAsync(string email, string password);

        public Task<List<Claim>> GetUserClaims(User user);
    }
}
