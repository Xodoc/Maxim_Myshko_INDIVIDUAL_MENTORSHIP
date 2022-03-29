using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUserService
    {
        public Task<IdentityUser> GetUserByUserCredentialsAsync(string email, string password);

        public Task<List<Claim>> GetUserClaims(IdentityUser user);
    }
}
