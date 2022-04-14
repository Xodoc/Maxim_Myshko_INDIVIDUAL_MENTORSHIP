using DAL.Entities;

namespace AuthenticationServer.Interfaces
{
    public interface ITokenService
    {
        Task<string> GetToken(User user);
    }
}
