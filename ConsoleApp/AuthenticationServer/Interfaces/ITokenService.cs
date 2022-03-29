using Microsoft.AspNetCore.Identity;

namespace AuthenticationServer.Interfaces
{
    public interface ITokenService
    {
        Task<string> GetToken(IdentityUser user);
    }
}
